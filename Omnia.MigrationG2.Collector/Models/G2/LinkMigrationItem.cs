using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class LinkMigrationItem : NavigationMigrationItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string backupFriendlyUrl { get; set; }
        public string targetUrl { get; set; }

    }
}
