using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Collector.Helpers
{
    public static class GlueHelper
    {
        public static List<string> GetPagesWithNonStaticBlocks(NavigationNode node)
        {
            List<string> list = new List<string>();
            RecursiveCheckNonStaticBlocks(node, list);

            return list;
        }

        public static List<string> GetPagesWithNonStaticBlocks(List<NewsItem> newsItems)
        {
            List<string> list = new List<string>();
            foreach(var newsItem in newsItems)
            {
                if (newsItem.IsPageInfoAvailable())
                {
                    var glueData = newsItem.PageInfo.GetGlueDataField();
                    if (glueData != null)
                    {
                        if (glueData.Controls.Any(i => i.IsStatic == false))
                        {
                            list.Add(newsItem.Url);
                        }
                    }
                }
            }

            return list;
        }

        public static List<string> GetPagesWithNoGlueDataField(NavigationNode node)
        {
            List<string> list = new List<string>();
            RecursiveCheckNoGlueDataField(node, list);

            return list;
        }

        public static List<string> GetPagesWithNoGlueDataField(List<NewsItem> newsItems)
        {
            List<string> list = new List<string>();
            foreach (var newsItem in newsItems)
            {
                if (newsItem.IsPageInfoAvailable())
                {
                    var glueData = newsItem.PageInfo.GetGlueDataField();
                    if (glueData == null)
                    {
                        list.Add(newsItem.Url);
                    }
                }
            }

            return list;
        }

        private static void RecursiveCheckNonStaticBlocks(NavigationNode node, List<string> list)
        {
            if (node.IsPage() && node.IsPageInfoAvailable())
            {
                var glueData = node.PageInfo.GetGlueDataField();
                if (glueData != null)
                {
                    if (glueData.Controls.Any(i => i.IsStatic == false))
                    {
                        list.Add(node.Url);
                    }
                }
            }

            foreach (var childNode in node.Children)
            {
                RecursiveCheckNonStaticBlocks(childNode, list);
            }
        }

        private static void RecursiveCheckNoGlueDataField(NavigationNode node, List<string> list)
        {
            if (node.IsPage() && node.IsPageInfoAvailable())
            {
                var glueData = node.PageInfo.GetGlueDataField();
                if (glueData == null)
                {
                    list.Add(node.Url);
                }
            }

            foreach (var childNode in node.Children)
            {
                RecursiveCheckNonStaticBlocks(childNode, list);
            }
        }
    }
}
