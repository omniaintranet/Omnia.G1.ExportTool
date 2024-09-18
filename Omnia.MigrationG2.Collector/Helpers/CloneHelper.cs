using Newtonsoft.Json;
using Omnia.MigrationG2.Collector.Models.G1;
using Omnia.MigrationG2.Collector.Models.G2;
using Omnia.MigrationG2.Collector.Models.MigrationItems;
using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Omnia.MigrationG2.Collector.Helpers
{
    // Clone helper to work around casting issue with OmniaJsonBase class
    public static class CloneHelper
    {
        public static PageNavigationMigrationItem CloneToPageMigration(NavigationMigrationItem navMigrationItem)
        {
            return Clone<PageNavigationMigrationItem>(navMigrationItem);
        }

        public static LinkNavigationMigrationItem CloneToLinkMigration(NavigationMigrationItem navMigrationItem)
        {
            return Clone<LinkNavigationMigrationItem>(navMigrationItem);
        }

        public static List<G1SearchProperty> CloneToG1CommonSearchProperty(List<SearchProperty> searchProperties)
        {
            return searchProperties.Select(x => Clone<G1SearchProperty>(x)).ToList();
        }

        public static T Clone<T>(object obj)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(obj));
        }
    }
}
