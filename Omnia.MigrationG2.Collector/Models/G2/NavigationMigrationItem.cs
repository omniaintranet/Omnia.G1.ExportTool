using System;
using System.Collections.Generic;
using System.Text;
using static Omnia.MigrationG2.Collector.Models.G2.Enums;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class NavigationMigrationItem
    {
        public int? Id { get; set; } // ignore

        public int? ParentId { get; set; } // ignore

        public List<NavigationMigrationItem> Children { get; set; }

        public bool ShowInMegaMenu { get; set; }

        public bool ShowInCurrentNavigation { get; set; }

        public NavigationMigrationItemTypes MigrationItemType { get; set; }

        public NavigationMigrationItem()
        {
            Children = new List<NavigationMigrationItem>();
        }
    }
}
