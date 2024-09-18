using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Collector.Models.Mappings;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.Reports
{
    public class PagesSummaryReport : BaseMigrationReport
    {
        #region Constructor

        static PagesSummaryReport()
        { }

        private PagesSummaryReport()
        {

        }

        public static PagesSummaryReport Instance { get; } = new PagesSummaryReport();

        #endregion

        public int NumberOfPagesWithDocumentRollup => PagesWithDocumentRollup.Count;

        public int NumberOfPagesWithControlledDocumentView => PagesWithControlledDocumentView.Count;

        public int NumberOfPagesWithPeopleRollup => PagesWithPeopleRollup.Count;

        public int NumberOfPagesWithBanner => PagesWithBanner.Count;
        public int NumberOfPagesWithReusableBanner => PagesWithReusableBanner.Count;
        public int NumberOfPagesWithHTMLScript => PagesWithHTMLScript.Count;
        public int NumberOfPageWithOtherBlocks => PagesWithOtherBlocks.Count;

        public HashSet<string> EnterpriseProperties { get; set; }

        public HashSet<SearchProperty> SearchProperties { get; set; }

        public HashSet<SearchPropertyMapping> SearchPropertyMappings { get; set; }

        public HashSet<string> PagesWithDocumentRollup { get; set; }

        public HashSet<string> PagesWithControlledDocumentView { get; set; }

        public HashSet<string> PagesWithPeopleRollup { get; set; }

        public HashSet<string> PagesWithBanner { get; set; }
        public HashSet<string> PagesWithReusableBanner { get; set; }
        public HashSet<string> PagesWithHTMLScript { get; set; }
        public HashSet<string> PagesWithOtherBlocks { get; set; }   
        
        public Dictionary<Guid, List<string>> PagesWithOtherBlocksDetails { get; set; }

        public HashSet<string> PagesUnderLink { get; set; }

        public Dictionary<Guid, HashSet<string>> CustomPageLayouts { get; set; }
        public Dictionary<Guid, HashSet<string>> CustomPageLayoutZones { get; set; }

        public HashSet<string> AllPages { get; set; }

        public override string ReportName => "PagesSummaryReport";

        public override void Init(string site)
        {
            base.Init(site);

            EnterpriseProperties = new HashSet<string>();
            SearchProperties = new HashSet<SearchProperty>();
            SearchPropertyMappings = new HashSet<SearchPropertyMapping>();
            PagesWithDocumentRollup = new HashSet<string>();
            PagesWithControlledDocumentView = new HashSet<string>();
            PagesWithPeopleRollup = new HashSet<string>();
            PagesWithBanner = new HashSet<string>();
            PagesWithReusableBanner = new HashSet<string>();
            PagesWithHTMLScript = new HashSet<string>();
            PagesWithOtherBlocks = new HashSet<string>();
            PagesWithOtherBlocksDetails = new Dictionary<Guid, List<string>>();
            PagesUnderLink = new HashSet<string>();
            CustomPageLayouts = new Dictionary<Guid, HashSet<string>>();
            CustomPageLayoutZones = new Dictionary<Guid, HashSet<string>>();
            AllPages = new HashSet<string>();
        }

        public void AddEnterpriseProperty(string property)
        {
            EnterpriseProperties.Add(property);
        }

        public void AddSearchProperty(SearchProperty property, string propertyDisplayName)
        {
            SearchProperties.Add(property);

            if (!SearchPropertyMappings.Any(x => x.G1PropertyId == property.Id))
            {
                SearchPropertyMappings.Add(new SearchPropertyMapping
                {
                    G1PropertyId = property.Id,
                    G1PropertyName = propertyDisplayName,
                    G2PropertyName = string.Empty,
                    ManagedPropertyName = property.ManagedProperty
                });
            }
        }

        public void AddPageWithOtherBlocks(string pageUrl, Guid blockId)
        {
            PagesWithOtherBlocks.Add(pageUrl);

            if (!PagesWithOtherBlocksDetails.ContainsKey(blockId))
            {
                PagesWithOtherBlocksDetails.Add(blockId, new List<string>());
            }

            PagesWithOtherBlocksDetails[blockId].Add(pageUrl);
        }

        public void AddCustomPageLayout(Guid pageLayoutId, List<GluePageControl> blockSettings, string pageUrl)
        {
            if (!CustomPageLayouts.ContainsKey(pageLayoutId))
            {
                CustomPageLayouts.Add(pageLayoutId, new HashSet<string>());
                CustomPageLayoutZones.Add(pageLayoutId, new HashSet<string>());
            }

            CustomPageLayouts[pageLayoutId].Add(pageUrl);

            foreach (var block in blockSettings)
            {
                CustomPageLayoutZones[pageLayoutId].Add(block.ZoneId);
            }
        }
    }
}
