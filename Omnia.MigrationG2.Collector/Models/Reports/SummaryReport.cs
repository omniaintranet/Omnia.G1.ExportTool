using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.Reports
{
    public class SummaryReport
    {
        public string SiteUrl { get; set; }
        public string backupFriendlyUrl { get; set; }
        public int NumberOfPages { get; set; }
        public int NumberOfSuccessCollectedPage { get; set; }
        public int NumberOfFailedCollectedPage { get; set; }
        //add number of checkout page - Diem
        public int NumberOfCheckOutPage { get; set; }
        public List<string> CheckOutPages { get; set; }

        public List<string> SuccessPages { get; set; }
        public List<FailedPagesModel> FailedPages { get; set; }
        public List<string> PagesWithNonStaticBlocks { get; set; }
        public List<string> PagesWithNoGlueDataField { get; set; }
        public List<PageProperty> ContentManagementProperties { get; set; }
        public List<string> SitesWithEmptyUserFieldEmail { get; set; }
    }

    public class FailedPagesModel
    {
        public string Url { get; set; }
        public string ErrorMessage { get; set; }
        public string Exception { get; set; }
    }
}
