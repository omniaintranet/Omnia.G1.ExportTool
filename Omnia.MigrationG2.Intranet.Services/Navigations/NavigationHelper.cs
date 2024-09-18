using Omnia.MigrationG2.Foundation.Core;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationHelper
    {
        private const int DefaultNavigationSourceDelayMinutes = 1440;
        private const int MinNavigationSourceDelayMinutes = 60;

        public static int GetNavigationSourceDelayTime(IConfigurationService configurationsClient)
        {
            int navigationSourceDelayTime = 0;
            var navigationSourceDelayTimeConfig = configurationsClient.GetConfiguration(
                Core.Constants.Configurations.Names.NavigationSourceDelayTime,
                Core.Constants.Configurations.Regions.NavigationSettings, Constants.Extensions.BuiltInExtensionPackageId);

            if (navigationSourceDelayTimeConfig != null)
            {
                navigationSourceDelayTime = Convert.ToInt32((string)navigationSourceDelayTimeConfig.Value);
            }

            return navigationSourceDelayTime < MinNavigationSourceDelayMinutes ? DefaultNavigationSourceDelayMinutes : navigationSourceDelayTime;
        }

        public static string BuildUrlWithNavigationSourceDelay<T>(INavigationNode<T> node)
        {
            return node.TargetUrl + String.Format(Constants.SharePoint.QueryStringTermFormat, node.TermStoreId.ToString(), node.TermSetId.ToString(), node.Id.ToString());
        }

        public static string GetNavigationTreeCacheKey(string navigationSourceUrl)
        {
            return string.Format("{0}-{1}", Intranet.Core.Constants.Caching.NavigationNodes, navigationSourceUrl);
        }

        public static string GetTopNodeCacheKey(string navigationSourceUrl, Models.Navigations.NavigationNode node)
        {
            return string.Format(Core.Constants.Caching.NavigationTopNode, navigationSourceUrl, node.TargetUrl, node.Id).ToLower();
        }

        public static string GetRootNavigationNodeCacheKey(string navigationSourceUrl)
        {
            return string.Format(Core.Constants.Caching.NavigationTopNodeList, navigationSourceUrl).ToLower();
        }

        public static string GetAllNodeHashesCacheKey(string navigationSourceUrl)
        {
            return string.Format(Core.Constants.Caching.AllNavigationTopNode, navigationSourceUrl).ToLower();
        }

        public static string GetNavigationLanguageKey(string navigationSourceUrl)
        {
            return string.Format(Core.Constants.Caching.NavigationLanguages, navigationSourceUrl).ToLower();
        }

        public static string GenerateTopNodeKey(string navigationSourceUrl, Models.Navigations.NavigationNode navigationNode)
        {
            Models.Navigations.NavigationNode topNode = null;
            return GenerateTopNodeKey(navigationSourceUrl, navigationNode, ref topNode);
        }

        public static string GenerateTopNodeKey(string navigationSourceUrl, Models.Navigations.NavigationNode navigationNode, ref Models.Navigations.NavigationNode topNode)
        {
            Guid navigationNodeId = navigationNode.Id;

            topNode = navigationNode;
            var rootNode = navigationNode;
            while (rootNode.Parent != null)
            {
                topNode = rootNode;
                rootNode = rootNode.Parent;
            }
            if (topNode == rootNode || rootNode.Children.Any(c => c.Id == navigationNodeId))
            {
                return GetRootNavigationNodeCacheKey(navigationSourceUrl);
            }
            else
                return GetTopNodeCacheKey(navigationSourceUrl, topNode);
        }
    }
}
