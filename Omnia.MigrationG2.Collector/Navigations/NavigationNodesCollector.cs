using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Core.Logger;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Services.Comments;
using Omnia.MigrationG2.Intranet.Services.ContentManagement;
using Omnia.MigrationG2.Intranet.Services.Navigations;

namespace Omnia.MigrationG2.Collector.Navigations
{
    public class NavigationNodesCollector : BaseCollectorService, INavigationNodesCollector
    {
        private readonly INavigationDBService _navigationDBService;
        private readonly IContentManagementService _contentManagementService;
        private readonly ICommentService _commentService;
        private List<string> NodesWithNonStaticBlocks = new List<string>();
        private List<string> NodesWithNoGlueData = new List<string>();
        //20211222 - Diem : get checkedout node
        private List<string> NodesWithCheckedOut = new List<string>();

        public List<string> GetNodesWithNonStaticBlocks()
        {
            return NodesWithNonStaticBlocks;
        }

        public List<string> GetNodesWithNoGlueData()
        {
            return NodesWithNoGlueData;
        }

        //20211222 - Diem : get check-out node
        public List<string> GetNodesWithCheckedOut()
        {
            return NodesWithCheckedOut;
        }

        public NavigationNodesCollector(
            INavigationDBService navigationDBService,
            IContentManagementService contentManagementService,
            ICommentService commentService)
        {
            _navigationDBService = navigationDBService;
            _contentManagementService = contentManagementService;
            _commentService = commentService;
        }

        public IList<NavigationNode> GetHierarchyNavigationNodes(string navigationSourceUrl, out int numberOfNavigationNode, DateTime? minDate = null,string navigationSiteUrl = null, string parentId = null)
        {
            return _navigationDBService.GetAllNavigationNodes(navigationSourceUrl, out numberOfNavigationNode, minDate,navigationSiteUrl,parentId);
        }

        public void LoadHierarchyNavigationNodesWithQuickPageInfo(string navigationSourceUrl, NavigationNode rootNode, CultureInfo cultureInfo, int numberOfNavigationNodes, ref int nodeIterator)
        {
            LoadQuickPageInfo(rootNode, navigationSourceUrl, cultureInfo, numberOfNavigationNodes, ref nodeIterator);
        }

        public List<ReportItemMessage> GetReports()
        {
            return Logger.GetReport();
        }

        private void LoadQuickPageInfo(NavigationNode node, string navigationSourceUrl, CultureInfo cultureInfo, int numberOfNavigationNodes, ref int nodeIterator)
        {
            var label = node.Labels.FirstOrDefault(i => i.LCID == cultureInfo.LCID);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Progress ({nodeIterator++}/{numberOfNavigationNodes})");
            Console.ForegroundColor = ConsoleColor.White;
            if (label != null)
                Logger.Info($"Checking node '{label.Value}' - '{node.Url}'...");
            else
                Logger.Info($"LCID not match in '{node.Url}'...");
            try
            {
                if (node.IsCustomLink())
                {
                    Logger.Success($"No target url found. Assume node as <Custom link>. Done.\n", node);
                }
                else if (node.IsPage())
                {
                    node.PageInfo = _contentManagementService.GetQuickPage(navigationSourceUrl, node, cultureInfo.LCID, cultureInfo.Name);
                    node.Comments = _commentService.GetComments(node.PageInfo.PageItemId, node.SiteUrl);
                    node.Likes = _commentService.GetLikeByPageId(node.PageInfo.PageItemId, node.SiteUrl);
                    node.TranslationPages = new List<NavigationNode>();
                    node.ListId = node.PageInfo.ListId;
                    if (node.IsPageInfoAvailable() && !node.PageInfo.CheckedOut) //20211222 - Diem : check-out page will not be imported
                    {
                        Logger.Info($"Loaded page info.");
                        var glueData = node.PageInfo.GetGlueDataField();
                        if (glueData == null)
                        {
                            NodesWithNoGlueData.Add(node.Url);
                            Logger.Info($"Page has <No Glue Data Field>.");
                        }
                        else
                        {
                            if (glueData.Controls.Any(i => i.IsStatic == false))
                                NodesWithNoGlueData.Add(node.Url);
                        }
                        node.PageInfoPropertiesSettings = _contentManagementService.GetQuickPagePropertiesSettings(node.PageInfo.SiteUrl, node.PageInfo.PageUrl, new Guid(node.PageInfo.PageListId));
                        if (node.PageInfo.TranslationPages != null)
                        {
                            if (node.PageInfo.TranslationPages.Count != 0)
                            {
                                foreach (var item in node.PageInfo.TranslationPages)
                                {
                                    var nodeItem = new NavigationNode()
                                    {
                                        Title = item.Title,
                                        PageInfo = item,
                                        Url = item.SiteUrl,
                                        Labels = node.Labels,
                                        ListId = item.ListId,
                                        Likes = _commentService.GetLikeByPageId(item.PageItemId, item.SiteUrl),
                                        Comments = _commentService.GetComments(item.PageItemId, item.SiteUrl)
                                    };
                                    try
                                    {
                                        nodeItem.PageInfoPropertiesSettings = _contentManagementService.GetQuickPagePropertiesSettings(navigationSourceUrl, nodeItem.PageInfo.PageUrl, new Guid(nodeItem.PageInfo.PageListId));
                                    }
                                    catch(AggregateException ex)
                                    {
                                        if (navigationSourceUrl.Equals(nodeItem.PageInfo.SiteUrl))
                                        {
                                            throw ex;
                                        }
                                        else
                                        {
                                            nodeItem.PageInfoPropertiesSettings = _contentManagementService.GetQuickPagePropertiesSettings(nodeItem.PageInfo.SiteUrl, nodeItem.PageInfo.PageUrl, new Guid(nodeItem.PageInfo.PageListId));
                                        }
                                    }
                                    node.TranslationPages.Add(nodeItem);
                                }
                            }
                        }
                        Logger.Success($"Loaded page properties. Done.\n", node);
                    }
                    else
                    {
                        string errorMessage = "";
                        if (node.PageInfo.IsAccessDenied)
                            errorMessage += "Access Denied; ";
                        if (node.PageInfo.IsPageNotExist)
                            errorMessage += "Page Not Exist; ";
                        if (node.PageInfo.IsSiteNotExist)
                            errorMessage += "Site Not Exist; ";
                        //20211222 - Diem : check-out page will not be imported
                        if (node.PageInfo.CheckedOut)
                            errorMessage += "Site Checked Out; ";
                        NodesWithCheckedOut.Add(node.Url);

                        Logger.Error(errorMessage, node);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Cannot check node.\n", node, ex);
            }
            foreach (var childNode in node.Children)
            {
                LoadQuickPageInfo(childNode, navigationSourceUrl, cultureInfo, numberOfNavigationNodes, ref nodeIterator);
            }
        }
    }
}
