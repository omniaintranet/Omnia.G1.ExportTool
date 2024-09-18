using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Omnia.MigrationG2.Collector.Banners;
using Omnia.MigrationG2.Collector.ExtensionMethods;
using Omnia.MigrationG2.Collector.Helpers;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.SharePoint;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Controls;
using Omnia.MigrationG2.Intranet.Models.Navigations;
using Omnia.MigrationG2.Intranet.Models.News;
using static Omnia.MigrationG2.Collector.Models.G2.Enums;
using static Omnia.MigrationG2.Core.Constants;
using NavigationNode = Omnia.MigrationG2.Intranet.Models.Navigations.NavigationNode;

namespace Omnia.MigrationG2.Collector.MigrationMapper
{
    public class MigrationMapper : IMigrationMapper
    {
        static readonly Guid PageRendererId = new Guid("8e012b42-4c13-4150-a11f-6b0b6300ee7c"); // ???

        private CultureInfo _cultureInfo;

        private ListDictionary listPages = new ListDictionary();
        private string spSiteURL;
        private CultureInfo CultureInfo
        {
            get
            {
                if (_cultureInfo == null)
                {
                    _cultureInfo = new CultureInfo(AppUtils.MigrationSettings.Language);
                }

                return _cultureInfo;
            }
        }

        private List<string> _specialList = new List<string>();

        public List<string> GetSpecialList()
        { 
            var list = _specialList.ToList();
            _specialList.Clear();
            return list;
        }

        #region Navigation Nodes
        public NavigationMigrationItem MapToNavigationMigrationItem(NavigationNode rootNode,string navigationSourceUrl,string sharePointUrl)
        {
            // we start as root node first so page type is page collection for G2

            var inputItem = RecursiveMap(rootNode, pageType: NavigationNodeType.PageCollection,navigationSourceUrl);            

            return UpdateURLForCustomLink(rootNode, pageType: NavigationNodeType.PageCollection, inputItem.Item2, navigationSourceUrl,sharePointUrl);
        }
       
        private NavigationMigrationItem UpdateURLForCustomLink(NavigationNode node, NavigationNodeType pageType, ListDictionary iPages, string navigationSourceUrl, string sharePointUrl)
        {
            NavigationMigrationItem migrationItem = null;
            spSiteURL = navigationSourceUrl;
            if (node.IsPage() && node.IsPageInfoAvailable() && node.IsPagePropertiesAvailable())
            {
                migrationItem = MapToPageMigrationItem(node, pageType);                
            }
            else if (node.IsCustomLink())
            {
                foreach(DictionaryEntry page in iPages)
                {
                    migrationItem = MapToLinkMigrationItem(node);

                    if(node.Url.ToLower().Contains(".aspx") && node.Url.ToLower().StartsWith(navigationSourceUrl))
                    {
                        var parts = node.Url.Split(sharePointUrl);                        
                        node.Url = parts.Last();                        
                    }
                    if (node.Url == page.Key.ToString())
                    {
                        migrationItem = MapToLinkMigrationItemIntranet(node,page.Value.ToString());
                        break;
                    }                                       
                }                
            }
            else
            {
                // fallback to empty node
                migrationItem = MapToEmptyPageMigrationItem(node, pageType);
            }

            if (migrationItem != null)
            {
                foreach (var childNode in node.Children)
                {
                    // children node will have page type as page in G2
                    var childMigrationItem = UpdateURLForCustomLink(childNode, pageType: NavigationNodeType.Page,iPages,navigationSourceUrl,sharePointUrl);
                    if (childMigrationItem != null)
                    {
                        migrationItem.Children.Add(childMigrationItem);
                    }
                }
            }
            return migrationItem;
        }
        private Tuple<NavigationMigrationItem, ListDictionary> RecursiveMap(NavigationNode node, NavigationNodeType pageType, string navigationSourceUrl)
        {
            NavigationMigrationItem migrationItem = null;
            
            if (node.IsPage() && node.IsPageInfoAvailable() && node.IsPagePropertiesAvailable())
            {
                migrationItem = MapToPageMigrationItem(node, pageType);
                try
                {
                    listPages.Add(node.TargetUrl, navigationSourceUrl + node.BackupFriendlyUrl);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Duplicated TargetUrl: " + node.TargetUrl);
                }
                
            }
            else if (node.IsCustomLink())
            {
                migrationItem = MapToLinkMigrationItem(node); 
            }
            else
            {
                // fallback to empty node
                migrationItem = MapToEmptyPageMigrationItem(node, pageType);
            }

            if (migrationItem != null)
            {
                foreach (var childNode in node.Children)
                {
                    // children node will have page type as page in G2
                    var childMigrationItem = RecursiveMap(childNode, pageType: NavigationNodeType.Page,navigationSourceUrl);
                    if (childMigrationItem != null)
                    {
                        migrationItem.Children.Add(childMigrationItem.Item1);
                    }
                }
            }

            return new Tuple<NavigationMigrationItem, ListDictionary>(migrationItem,listPages);
        }

        private LinkMigrationItem MapToLinkMigrationItem(NavigationNode node)
        {
            LinkMigrationItem item = new LinkMigrationItem();
            item.MigrationItemType = NavigationMigrationItemTypes.Link;
            item.Title = CollectorMethods.GetNodeLabel(node, CultureInfo);
            item.Url = node.CustomLinkUrl;
            item.backupFriendlyUrl = node.BackupFriendlyUrl;
            item.targetUrl = node.TargetUrl;
            return item;
        }
        private LinkMigrationItem MapToLinkMigrationItemIntranet(NavigationNode node, string url)
        {
            LinkMigrationItem item = new LinkMigrationItem();
            item.MigrationItemType = NavigationMigrationItemTypes.Link;
            item.Title = CollectorMethods.GetNodeLabel(node, CultureInfo);
            item.Url = url;                        
            return item;
        }

        private PageMigrationItem MapToEmptyPageMigrationItem(NavigationNode node, NavigationNodeType pageType)
        {
            PageMigrationItem item = new PageMigrationItem();
            item.MigrationItemType = NavigationMigrationItemTypes.Page;
            item.UrlSegment = node.FriendlyUrlSegment;
            item.TargetUrl = node.TargetUrl;

            item.GlueLayoutId = null;
            item.PageData = new PageData()
            {
                Type = NavigationNodeType.Page,
                PageRendererId = MigrationMapper.PageRendererId,
                Title = CollectorMethods.GetNodeLabel(node, CultureInfo),
                Layout = null, // ???
                EnterpriseProperties = null
            };

            return item;
        }

        private PageMigrationItem MapToPageMigrationItem(NavigationNode node, NavigationNodeType pageType)
        {
            PageMigrationItem item = new PageMigrationItem();
            item.BlockSettings = new List<GluePageControl>();
            item.MigrationItemType = NavigationMigrationItemTypes.Page;
            item.UrlSegment = node.FriendlyUrlSegment;
            item.TargetUrl = node.TargetUrl;
            item.ListId = node.ListId;
            item.PageLanguage = node.PageInfo.PageLanguageLocale;
            item.ShowInCurrentNavigation = node.ShowInCurrent;
            item.ShowInMegaMenu = node.ShowInGlobal;
            item.PhysicalPageUniqueId = node.PageInfo.PhysicalPageUniqueId;
            item.Id = node.PageInfo.PageItemId;
            var glueData = node.PageInfo.GetGlueDataField();
            if (glueData != null)
            {
                item.GlueLayoutId = glueData.LayoutId;
                item.BlockSettings.AddRange(GetGluePageControl(glueData.Controls));
            }

            if (node.PageInfo.HasCustomDataField)
            {
                item.RelatedLinks = RelatedLinkHelper.InitRelatedLink(node.PageInfo,spSiteURL, listPages);
            } 
            item.PageData = InitPageData(node, pageType);
            item.Comments = node.Comments;
            item.Likes = node.Likes;
            item.TranslationPages = new List<PageMigrationItem>();
            if (node.TranslationPages != null)
            {
                foreach (var nodeItem in node.TranslationPages)
                {
                    var data = MapToPageMigrationItem(nodeItem, pageType);
                    item.TranslationPages.Add(data);
                }
            }
            return item;
        }

        private PageData InitPageData(NavigationNode node, NavigationNodeType pageType)
        {
            PageData pageData = new PageData();
            pageData.Type = pageType;
            pageData.PageRendererId = MigrationMapper.PageRendererId;
            pageData.Title = CollectorMethods.GetNodeLabel(node, CultureInfo.GetCultureInfo(node.PageInfo.PageLanguageLocale));
            pageData.Layout = null; // ???

            var flag = true;
            pageData.EnterpriseProperties = InitEnterpriseProperties(node.PageInfo, node.PageInfoPropertiesSettings, node.PageInfo.Context, out flag);
            if (!flag)
            {
                _specialList.Add(node.Url);
            }

            return pageData;
        }
        #endregion Navigation Nodes

        #region News Articles
        public NavigationMigrationItem MapToNavigationMigrationItem(List<NewsItem> newsItems, string newsCenterUrl, string newsCenterTitle = null)
        {
            var root = InitEmptyRoot(newsCenterUrl, newsCenterTitle);
            foreach (var newsItem in newsItems)
            {
                root.Children.Add(LoopMap(newsItem));
            }

            return root;
        }

        private NavigationMigrationItem InitEmptyRoot(string newsCenterUrl, string newsCenterTitle = null)
        {
            PageMigrationItem item = new PageMigrationItem();
            item.MigrationItemType = NavigationMigrationItemTypes.Page;
            item.UrlSegment = GetUrlSegment(newsCenterUrl);
            item.GlueLayoutId = null;
            item.PageData = new PageData()
            {
                Type = NavigationNodeType.PageCollection,
                PageRendererId = MigrationMapper.PageRendererId,
                Title = string.IsNullOrEmpty(newsCenterTitle) ? item.UrlSegment : newsCenterTitle,
                Layout = null, // ???
                EnterpriseProperties = null
            };
            return item;
        }

        private NavigationMigrationItem LoopMap(NewsItem newsItem)
        {
            NavigationMigrationItem migrationItem = null;

            if (newsItem.IsPageInfoAvailable() && newsItem.IsPagePropertiesAvailable())
            {
                migrationItem = MapToPageMigrationItem(newsItem);
            }
            else
            {
                // fallback to empty node
                migrationItem = MapToEmptyPageMigrationItem(newsItem);
            }

            return migrationItem;
        }

        private PageMigrationItem MapToPageMigrationItem(NewsItem newsItem)
        {
            PageMigrationItem item = new PageMigrationItem();
            item.BlockSettings = new List<GluePageControl>();
            item.MigrationItemType = NavigationMigrationItemTypes.Page;
            item.UrlSegment = GetUrlSegment(newsItem.Url);
            item.ListId = newsItem.ListId;
            item.PageLanguage = newsItem.PageInfo.PageLanguageLocale;
            item.PhysicalPageUniqueId = newsItem.PageInfo.PhysicalPageUniqueId;
            item.Id = newsItem.Id;
            var glueData = newsItem.PageInfo.GetGlueDataField();
            if (glueData != null)
            {
                item.GlueLayoutId = glueData.LayoutId;
                item.BlockSettings.AddRange(GetGluePageControl(glueData.Controls));
            }

            if (newsItem.PageInfo.HasCustomDataField)
            {
                item.RelatedLinks = RelatedLinkHelper.InitRelatedLink(newsItem.PageInfo, spSiteURL, listPages);
            }

            item.PageData = InitPageData(newsItem);
            item.Comments = newsItem.Comments;
            item.Likes = newsItem.Likes;
            item.TranslationPages = new List<PageMigrationItem>();
            if (newsItem.TranslationPages != null)
            {
                foreach (var page in newsItem.TranslationPages)
                {
                    var data = MapToPageMigrationItem(page);
                    item.TranslationPages.Add(data);
                }
            }
            return item;
        }

        private PageMigrationItem MapToEmptyPageMigrationItem(NewsItem newsItem)
        {
            PageMigrationItem item = new PageMigrationItem();
            item.MigrationItemType = NavigationMigrationItemTypes.Page;
            item.UrlSegment = GetUrlSegment(newsItem.Url);
            item.GlueLayoutId = null;
            item.PageData = new PageData()
            {
                Type = NavigationNodeType.Page,
                PageRendererId = MigrationMapper.PageRendererId,
                Title = newsItem.Title,
                Layout = null, // ???
                EnterpriseProperties = null
            };

            return item;
        }

        private PageData InitPageData(NewsItem newsItem)
        {
            PageData pageData = new PageData();
            pageData.Type = NavigationNodeType.Page;
            pageData.PageRendererId = MigrationMapper.PageRendererId;
            pageData.Title = newsItem.Title;
            pageData.Layout = null; // ???

            var flag = true;
            pageData.EnterpriseProperties = InitEnterpriseProperties(newsItem.PageInfo, newsItem.PageInfoPropertiesSettings, newsItem.PageInfo.Context, out flag);
            if (!flag)
            {
                _specialList.Add(newsItem.Url);
            }
            return pageData;
        }

        private string GetUrlSegment(string url)
        {
            var parts = url.Split("/");
            var urlSegment = parts.Last().Replace(".aspx", "");
            return urlSegment;
        }

        private BannerSettings MappingBannerFromDB(Intranet.Models.Banner.LinkedInformationContent item)
        { 
            BannerSettings ret = new BannerSettings()
            {
                backgroundColor = item.BackgroundColor,
                content = item.Content,
                contentColor = item.ContentColor,
                footer = item.Footer,
                footerColor = item.FooterColor,
                imageUrl = item.ImageUrl,
                isOpenLinkNewWindow = item.IsOpenLinkNewWindow,
                linkUrl = item.LinkUrl,
                opacity = item.Opacity,
                title = item.Title,
                titleColor = item.TitleColor,
                titleSettings = item.TitleSettings != null ? JsonConvert.DeserializeObject<TitleSettings>(item.TitleSettings.ToString()) : null
            };
            return ret;
        }

        private object BannerSettingsMapping(string settings)
        {
            var bannerSettings = JsonConvert.DeserializeObject<BannerSettings>(settings);
            var bannerCollector = AppUtils.GetService<IReusableBannersCollector>();
            if (bannerSettings.bannerType == "1" && bannerSettings.templateName != "")
            {
                var linkedItem = bannerCollector.GetLinkedInformationContentsByName(bannerSettings.templateName);
                return MappingBannerFromDB(linkedItem);
            }
            else if(bannerSettings.bannerType == "2")
            {
                var groupLinkedItem = bannerCollector.GetGroupLinkedInformationContentsByName(bannerSettings.groupName);
                foreach(var linkedItemName in groupLinkedItem.LinkedInformationNames)
                { 
                    var linkedItem = bannerCollector.GetLinkedInformationContentsByName(linkedItemName);
                    return MappingBannerFromDB(linkedItem);
                }
            }
            
            return bannerSettings;
        }

        private List<GluePageControl> GetGluePageControl(List<GluePageControl> controls)
        {
            controls = controls.Where(control => control.Settings != null).Distinct().ToList();
            var listControlSettings = new List<GluePageControl>();
            foreach (var control in controls)
            {
                try
                {
                    switch (control.ControlId)
                    {
                        case Intranet.Core.Constants.ControlSettingsKey.Banner:
                            control.Settings = BannerSettingsMapping(control.Settings.ToString());
                            break;
                        case Intranet.Core.Constants.ControlSettingsKey.PeopleRollUp:
                            control.Settings = JsonConvert.DeserializeObject<PeopleRollUpSettings>(control.Settings.ToString());
                            break;
                        case Intranet.Core.Constants.ControlSettingsKey.DocumentRollUp:
                            control.Settings = JsonConvert.DeserializeObject<DocumentRollUpSettings>(control.Settings.ToString());
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                }
               
                listControlSettings.Add(control);
            }

            return listControlSettings;        
        }
        #endregion News Articles

        #region Shared
        private Dictionary<string, object> InitEnterpriseProperties(PageInfo pageInfo, PageInfoPropertiesSettings pageInfoPropertiesSettings, ExtendedClientContext context, out bool flag)
        {
            Dictionary<string, object> enterpriseProperties = new Dictionary<string, object>();

            bool flag1 = true;
            if (AppUtils.MigrationSettings.UseSettingsInContentManagement)
            {
                flag1 = EnterprisePropertiesHelper.InitContentManagementProperties(enterpriseProperties, pageInfoPropertiesSettings, context);
            }

            bool flag2 = EnterprisePropertiesHelper.InitDefaultProperties(enterpriseProperties, pageInfo);

            bool flag3 = EnterprisePropertiesHelper.InitCustomProperties(enterpriseProperties, pageInfoPropertiesSettings, context);

            flag = flag1 && flag2 && flag3;

            return enterpriseProperties;
        }
        #endregion Shared      
    }
}
