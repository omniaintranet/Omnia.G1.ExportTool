using Omnia.MigrationG2.Collector.Models.G2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.MigrationItems
{
    public class LinkNavigationMigrationItem : NavigationMigrationItem
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }
}
