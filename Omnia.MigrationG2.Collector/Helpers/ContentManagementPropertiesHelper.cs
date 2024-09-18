using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Collector.Helpers
{
    public static class ContentManagementPropertiesHelper
    {
        public static List<PageProperty> GetContentManagementProperties(NavigationNode rootNode)
        {
            List<PageProperty> list = new List<PageProperty>();
            RecursiveCheck(rootNode, list);

            list = list.GroupBy(i => i.InternalName).Select(i => i.First()).ToList();

            return list;
        }

        public static List<PageProperty> GetContentManagementProperties(List<NewsItem> newsItems)
        {
            List<PageProperty> list = new List<PageProperty>();
            foreach (var newsItem in newsItems)
            {
                if (newsItem.IsPagePropertiesAvailable())
                {
                    list.AddRange(newsItem.PageInfoPropertiesSettings
                        .PagePropertiesSetting
                        .Where(i => i.IsMigrationG2Property == false));
                }
            }

            list = list.GroupBy(i => i.InternalName).Select(i => i.First()).ToList();

            return list;
        }

        private static void RecursiveCheck(NavigationNode node, List<PageProperty> list)
        {
            if (node.IsPage() && node.IsPagePropertiesAvailable())
            {
                list.AddRange(node.PageInfoPropertiesSettings
                    .PagePropertiesSetting
                    .Where(i => i.IsMigrationG2Property == false));
            }

            foreach (var childNode in node.Children)
            {
                RecursiveCheck(childNode, list);
            }
        }
    }
}
