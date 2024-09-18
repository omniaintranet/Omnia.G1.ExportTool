using Newtonsoft.Json;
using Omnia.MigrationG2.Collector.Helpers;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Collector.Models.MigrationItems;
using Omnia.MigrationG2.Collector.Models.Reports;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using Omnia.MigrationG2.Intranet.Models.Shared;
using Omnia.MigrationG2.Intranet.Services.SearchProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Omnia.MigrationG2.Models.AppSettings;
using static Omnia.MigrationG2.Collector.Models.G2.Enums;
using Omnia.MigrationG2.Collector.Http;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Intranet.Models.Controls;

namespace Omnia.MigrationG2.Collector.Summary
{
    public class SummaryDataCollector : BaseCollectorService, ISummaryDataCollector
    {
        private readonly ISearchPropertyService _searchpropsService;
        private ODMHttpClient _ODMHttpClient { get; }
        private MigrationSettings _migrationSettings { get; set;}
        private List<G1SearchProperty> G1SearchProperties { get; set; }

        public SummaryDataCollector(
            ISearchPropertyService searchpropsService, 
            ODMHttpClient ODMHttpClient
            )
        {
            _searchpropsService = searchpropsService;
            _ODMHttpClient = ODMHttpClient;        
        }

        public void getData(NavigationMigrationItem input, string siteName, MigrationSettings migrationSettings)
        {
            var inputArray = new List<NavigationMigrationItem>();
            inputArray.Add(input);
            getData(inputArray, siteName, migrationSettings);
        }

        public void getData(List<NavigationMigrationItem> input,string siteName, MigrationSettings migrationSettings)
        {
            _migrationSettings = migrationSettings;
            input.GetTotalCount();
            PagesSummaryReport.Instance.Init(siteName);

            G1SearchProperties = GetG1SearchProperties();

            foreach (var item in input)
            {
                CheckPage(item, string.Empty);
            }
            PagesSummaryReport.Instance.ExportTo($"{Directory.GetCurrentDirectory()}\\ExportData\\Reports");
        }

        private List<G1SearchProperty> GetG1SearchProperties()
        {
            List<G1SearchProperty> result = new List<G1SearchProperty>();
            result = CloneHelper.CloneToG1CommonSearchProperty(_searchpropsService.GetSearchProperties(0));
            if (_ODMHttpClient.isODMSettingsEnsured)
            {
                var getODMSearchPropertyResult = _ODMHttpClient.GetSearchPropertiesAsync().Result;
                result.AddRange(getODMSearchPropertyResult.Data);
            }

            return result;
        }

        private void CheckPage(NavigationMigrationItem navigationMigrationItem, string parentPageUrl)
        {
            string pageUrl = string.Empty;
            if (navigationMigrationItem.MigrationItemType == NavigationMigrationItemTypes.Page)
            {
                var pageNode = CloneHelper.CloneToPageMigration(navigationMigrationItem);
                pageUrl = $"{parentPageUrl}/{pageNode.UrlSegment}";

                PagesSummaryReport.Instance.AllPages.Add(pageUrl);

                CheckPageBlocks(pageNode, pageUrl);
                CheckPageLayout(pageNode, pageUrl);
                CheckEnterpriseProperties(pageNode);
                CheckSearchProperties(pageNode);
            }
            else if (navigationMigrationItem.MigrationItemType == NavigationMigrationItemTypes.Link)
            {
                var linkNode = CloneHelper.CloneToLinkMigration(navigationMigrationItem);
                pageUrl = $"{parentPageUrl}/{linkNode.Title?.ToLower().Replace(" ", "-")}";

                CheckPagesUnderLink(linkNode, pageUrl);
            }

            PagesSummaryReport.Instance.AllPages.Add(pageUrl);

            foreach (var item in navigationMigrationItem.Children)
            {
                CheckPage(item, pageUrl);
            }
        }

        private void CheckPagesUnderLink(LinkNavigationMigrationItem linkNode, string pageUrl)
        {
            var childPages = linkNode.Children.Where(x => x.MigrationItemType == NavigationMigrationItemTypes.Page).ToList();
            foreach (var child in childPages)
            {
                PagesSummaryReport.Instance.PagesUnderLink.Add(pageUrl);
            }
        }

        private void CheckPageBlocks(PageNavigationMigrationItem page, string pageUrl)
        {
            foreach (var block in page.BlockSettings)
            {
                switch (block.ControlId.ToString().ToUpper())
                {
                    case Constants.G1ControlIDs.BannerIdString:
                        PagesSummaryReport.Instance.PagesWithBanner.Add(pageUrl);
                        var bannerSettings = JsonConvert.DeserializeObject<BannerSettings>(block.Settings.ToString());
                        if(bannerSettings.bannerType !="0")//reusable banner
                        {
                            PagesSummaryReport.Instance.PagesWithReusableBanner.Add(pageUrl);
                        }
                        break;
                    case Constants.G1ControlIDs.DocumentRollupIdString:
                        PagesSummaryReport.Instance.PagesWithDocumentRollup.Add(pageUrl);
                        break;
                    case Constants.G1ControlIDs.ControlledDocumentViewIdString:
                        PagesSummaryReport.Instance.PagesWithControlledDocumentView.Add(pageUrl);
                        break;
                    case Constants.G1ControlIDs.PeopleRollupIdString:
                        PagesSummaryReport.Instance.PagesWithPeopleRollup.Add(pageUrl);
                        break;
                    case Constants.G1ControlIDs.ScriptHtmlIdString:
                        PagesSummaryReport.Instance.PagesWithHTMLScript.Add(pageUrl);
                        break;
                    default:
                        PagesSummaryReport.Instance.AddPageWithOtherBlocks(pageUrl, new Guid(block.ControlId));
                        break;
                }
            }
        }

        private void CheckPageLayout(PageNavigationMigrationItem page, string pageUrl)
        {
            if (page.GlueLayoutId != null && IsCustomLayout(page.GlueLayoutId.Value))
            {
                PagesSummaryReport.Instance.AddCustomPageLayout(page.GlueLayoutId.Value, page.BlockSettings, pageUrl);
            }
        }

        private void CheckEnterpriseProperties(PageNavigationMigrationItem page)
        {
            if (page.PageData != null && page.PageData.EnterpriseProperties != null)
            {
                foreach (var property in page.PageData.EnterpriseProperties.Keys)
                {
                    PagesSummaryReport.Instance.AddEnterpriseProperty(property);
                }
            }
        }

        private void CheckSearchProperties(PageNavigationMigrationItem page)
        {
            if (page.BlockSettings != null)
            {
                var blockSettingsJson = JsonConvert.SerializeObject(page.BlockSettings);
                G1SearchProperties = G1SearchProperties.Where(p => p.DisplayName != null).ToList();
                foreach (var prop in G1SearchProperties)
                {
                    var propNames = JsonConvert.DeserializeObject<List<LocalizedText>>(prop.DisplayName);
                    var defaultPropName = propNames.FirstOrDefault(x => x.language == _migrationSettings.Language)?.value;

                    if (string.IsNullOrEmpty(defaultPropName))
                        defaultPropName = propNames.FirstOrDefault(x => string.IsNullOrEmpty(x.language))?.value;

                    if (string.IsNullOrEmpty(defaultPropName))
                        defaultPropName = propNames.FirstOrDefault()?.value;

                    if (propNames.Any(x => blockSettingsJson.Contains("{Property." + x.value + "}")))
                    {
                        PagesSummaryReport.Instance.AddSearchProperty(prop, defaultPropName);
                    }

                    if (blockSettingsJson.Contains(prop.Id.ToString()) || blockSettingsJson.Contains(prop.Id.ToString().ToLower()))
                    {
                        PagesSummaryReport.Instance.AddSearchProperty(prop, defaultPropName);
                    }
                }
            }
        }

        private bool IsCustomLayout(Guid pageLayoutId)
        {
            return pageLayoutId != Constants.G1GlueLayoutIDs.PageWithLeftNav &&
                pageLayoutId != Constants.G1GlueLayoutIDs.PageWithoutLeftNav &&
                pageLayoutId != Constants.G1GlueLayoutIDs.StartPage &&
                pageLayoutId != Constants.G1GlueLayoutIDs.NewsArticle;
        }
    }
}
