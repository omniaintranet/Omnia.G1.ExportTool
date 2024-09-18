using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.Mappings
{
    public class SearchPropertyMapping
    {
        public string G1PropertyName { get; set; }

        public Guid G1PropertyId { get; set; }

        public string G2PropertyName { get; set; }

        public string ManagedPropertyName { get; set; }
    }
}
