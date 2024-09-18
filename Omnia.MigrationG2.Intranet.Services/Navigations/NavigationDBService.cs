using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Core.Configurations;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Repositories.Navigations;
using Omnia.MigrationG2.Intranet.Services.ContentManagement;

namespace Omnia.MigrationG2.Intranet.Services.Navigations
{
    public class NavigationDBService : INavigationDBService
    {
        private readonly INavigationNodeRepository _navigationNodeRepository;
        private readonly INavigationTermService _navigationTermService;
        private readonly IConfigurationService _configurationService;

        public NavigationDBService(
            INavigationNodeRepository navigationNodeRepository,
            INavigationTermService navigationTermService,
            IConfigurationService configurationService)
        {
            _navigationNodeRepository = navigationNodeRepository;
            _navigationTermService = navigationTermService;
            _configurationService = configurationService;
        }

        public IList<NavigationNode> GetAllNavigationNodes(string navigationSourceUrl, out int numberOfNavigationNodes,DateTime? minDate = null, string navigationSiteUrl=null, string parentId = null)
        {
            try
            {
                IList<NavigationNode> navigationNodesFromDb = null;
                if (string.IsNullOrEmpty(navigationSiteUrl))
                {
                    navigationNodesFromDb = _navigationNodeRepository.GetByNavigationSourceUrl(navigationSourceUrl);
                }
                else
                {
                    navigationNodesFromDb = _navigationNodeRepository.GetByNavigationSiteUrl(navigationSourceUrl,navigationSiteUrl,parentId);
                }
                
                if(minDate != null)
                {
                    DateTimeOffset minOffDate = minDate.Value;
                    navigationNodesFromDb = navigationNodesFromDb.Where(navNode => minOffDate.CompareTo(navNode.CreatedAt) <= 0).ToList();
                }
                numberOfNavigationNodes = navigationNodesFromDb.Count;

                var navigationSourceDelayTime = NavigationHelper.GetNavigationSourceDelayTime(_configurationService);
                var navigationLanguages = _navigationTermService.GetNavigationLanguages();
                var isSupportLegalcyWebpart = GetSupportLegacyWebpartSetting(navigationSourceUrl);

                navigationNodesFromDb = BuildHierarchy(navigationSourceUrl, navigationNodesFromDb, navigationSourceDelayTime, setDuplicatedTarget: true, setSupportLegalcyWebpart: isSupportLegalcyWebpart);
                return navigationNodesFromDb;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private bool GetSupportLegacyWebpartSetting(string navigationSourceUrl = null)
        {
            bool isSupportLegacyWebpart = false;

            var quickPageSettings = GetQuickPageSettings(navigationSourceUrl);
            if (quickPageSettings != null)
            {
                if (quickPageSettings.EnableSetting == Models.ContentManagement.EnableQuickPageOptions.Disable)
                    isSupportLegacyWebpart = true;
            }

            return isSupportLegacyWebpart;
        }

        public QuickPageSettings GetQuickPageSettings(string navigationSourceUrl = null)
        {
            try
            {
                QuickPageSettings settings = null;
                var configuration = InitQuickPageSettingsConfiguration(navigationSourceUrl);
                var configurationData = _configurationService.GetConfiguration(configuration.Name, configuration.Region);
                if (configurationData != null && configurationData.Value != null)
                {
                    settings = JsonConvert.DeserializeObject<QuickPageSettings>(configurationData.Value);
                }

                return settings;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private Foundation.Models.Configurations.Configuration InitQuickPageSettingsConfiguration(string navigationSourceUrl = null)
        {
            //Quick page settings has Site collection scope
            return new Foundation.Models.Configurations.Configuration()
            {
                Name = "quickpagesettings",
                Region = navigationSourceUrl,
                Value = string.Empty
            };
        }

        private List<Models.Navigations.NavigationNode> BuildHierarchy(string navigationSourceUrl, IList<Models.Navigations.NavigationNode> sourceNodes,
            int navigationSourceDelayTime, Dictionary<Guid, Models.Navigations.NavigationNode> nodesDict = null,
            bool setDuplicatedTarget = false, bool setSupportLegalcyWebpart = false)
        {
            HandleDupplicatedSortOrder(sourceNodes);
            var retVal = new List<Models.Navigations.NavigationNode>();
            if (nodesDict == null)
                nodesDict = sourceNodes.ToDictionary(n => n.Id);

            var processedNodeIds = new List<Guid>();
            Dictionary<string, IList<Guid>> targetDict = null;
            if (setDuplicatedTarget)
                targetDict = new Dictionary<string, IList<Guid>>();

            foreach (var item in sourceNodes)
            {
                AddRootNodeRecursive(item, navigationSourceDelayTime, retVal, nodesDict, processedNodeIds, targetDict: targetDict, setSupportLegalcyWebpart: setSupportLegalcyWebpart);
            }
            SetDuplicatedTargetValues(navigationSourceUrl, targetDict, nodesDict);
            EnsureSortOrder(retVal.ToArray());
            return retVal;
        }

        private bool EnsureSortOrder<T>(params T[] rootNodes) where T : INavigationNode<T>
        {
            var haveUpdate = false;
            foreach (var item in rootNodes)
            {
                if (item.Children != null && item.Children.Count > 0)
                {
                    haveUpdate = true;
                    if (item.Children.Any(i => i.CreatedAt != null))
                        item.Children = item.Children.OrderBy(i => i.CreatedAt).ToList();

                    if (!string.IsNullOrEmpty(item.CustomSortOrder))
                    {
                        var childrenNodes = item.Children.ToList();
                        var listOrderNode = new List<T>();
                        var arrayNodeIds = item.CustomSortOrder.Split(':');
                        for (int i = 0; i < arrayNodeIds.Length; i++)
                        {
                            var orderNode = childrenNodes.Where(n => n.Id.ToString().ToLower() == arrayNodeIds[i].ToLower())
                                .FirstOrDefault();
                            if (orderNode != null)
                                listOrderNode.Add(orderNode);
                        }
                        if (listOrderNode.Count > 0)
                        {
                            var notSortedNodes = childrenNodes.Where(i => !listOrderNode.Any(orderItem => orderItem.Id == i.Id)).ToList();
                            item.Children.Clear();
                            item.Children.AddRange(listOrderNode);
                            item.Children.AddRange(notSortedNodes);
                        }
                    }
                    EnsureSortOrder(item.Children.ToArray());
                }
            }
            return haveUpdate;
        }

        private void SetDuplicatedTargetValues<T>(string navigationSourceUrl, Dictionary<string, IList<Guid>> targetDict,
            Dictionary<Guid, T> nodesDict) where T : INavigationNode<T>
        {
            if (targetDict != null)
            {
                var configurations = _configurationService.GetConfigurationsInRegion(Core.Utils.CommonUtils.GetSetASDefaultConfigurationRegion(navigationSourceUrl));


                var duplicatedTargets = targetDict.Where(i => i.Value.Count > 1);
                foreach (var target in duplicatedTargets)
                {
                    foreach (var nodeId in target.Value)
                    {
                        var configuration = configurations.FirstOrDefault(c => c.GetValue() == nodeId.ToString().ToLower());
                        nodesDict[nodeId].IsDuplicateTargetUrl = configuration == null;
                    }
                }
            }
        }

        private void HandleDupplicatedSortOrder(IList<Models.Navigations.NavigationNode> navigationNodes)
        {
            foreach (var node in navigationNodes)
            {
                if (!string.IsNullOrEmpty(node.CustomSortOrder))
                {
                    var customSortOrderArr = node.CustomSortOrder.Split(':');
                    var customSortOrder = string.Join(":", customSortOrderArr.Distinct());
                    node.CustomSortOrder = customSortOrder;
                }
            }
        }

        private void AddRootNodeRecursive(Models.Navigations.NavigationNode currentNode,
            int navigationSourceDelayTime,
            IList<Models.Navigations.NavigationNode> rootNodes,
            Dictionary<Guid, Models.Navigations.NavigationNode> nodesDict,
            List<Guid> processedNodeIds,
            Dictionary<string, IList<Guid>> targetDict = null, bool setSupportLegalcyWebpart = false)
        {
            if (processedNodeIds.Contains(currentNode.Id))
                return;

            processedNodeIds.Add(currentNode.Id);
            if (!string.IsNullOrWhiteSpace(currentNode.TargetUrl) && targetDict != null)
            {
                var lowerTargetUrl = currentNode.TargetUrl.ToLower();
                if (!targetDict.ContainsKey(lowerTargetUrl))
                    targetDict.Add(lowerTargetUrl, new List<Guid>());

                targetDict[lowerTargetUrl].Add(currentNode.Id);
            }
            if (nodesDict.ContainsKey(currentNode.ParentId))
            {
                var parentNode = nodesDict[currentNode.ParentId];
                parentNode.Children.Add(currentNode);
                currentNode.Parent = parentNode;
                AddRootNodeRecursive(parentNode, navigationSourceDelayTime, rootNodes, nodesDict, processedNodeIds,
                    targetDict: targetDict);
                currentNode.Level = parentNode.Level + 1;
                if (setSupportLegalcyWebpart && string.IsNullOrEmpty(currentNode.CustomLinkUrl))
                    currentNode.SupportLegacyWebpart = true;
                SetFriendlyUrl(currentNode, parentNode);
                HandleNavigationUrl(currentNode, navigationSourceDelayTime, parentNode.ForceChildrenUseDelayUrl);
            }
            else
            {
                currentNode.Level = 1;
                if (setSupportLegalcyWebpart && string.IsNullOrEmpty(currentNode.CustomLinkUrl))
                    currentNode.SupportLegacyWebpart = true;
                SetFriendlyUrl(currentNode, null);
                HandleNavigationUrl(currentNode, navigationSourceDelayTime, false);
                rootNodes.Add(currentNode);
            }
        }

        private void HandleNavigationUrl<T>(INavigationNode<T> navigationNode, int navigationSourceDelay, bool forceUseDelayUrl)
        {
            string url = string.Empty;
            if (!string.IsNullOrEmpty(navigationNode.CustomLinkUrl))
            {
                url = navigationNode.CustomLinkUrl;
            }
            else if (!navigationNode.SupportLegacyWebpart.HasValue || !navigationNode.SupportLegacyWebpart.Value)
            {
                url = navigationNode.BackupFriendlyUrl;
            }
            else
            {
                bool useDelayUrl = false;
                if (forceUseDelayUrl)
                    useDelayUrl = true;
                else if (navigationSourceDelay > 0)
                {
                    if (DateTime.Compare(navigationNode.LastModifiedDate.AddMinutes(navigationSourceDelay).ToUniversalTime(), DateTime.UtcNow.ToUniversalTime()) > 0)
                    {
                        useDelayUrl = true;
                    }
                }
                if (useDelayUrl)
                {
                    url = NavigationHelper.BuildUrlWithNavigationSourceDelay(navigationNode);
                    navigationNode.ForceChildrenUseDelayUrl = true;
                }
                else
                    url = navigationNode.BackupFriendlyUrl;
            }
            navigationNode.Url = url;
        }

        private void SetFriendlyUrl<T>(INavigationNode<T> node, INavigationNode<T> parentNode)
        {
            var friendlyUrl = $"/{node.FriendlyUrlSegment}";
            if (parentNode != null)
            {
                friendlyUrl = $"{parentNode.BackupFriendlyUrl}{friendlyUrl}";
            }
            node.BackupFriendlyUrl = friendlyUrl;
        }
    }
}
