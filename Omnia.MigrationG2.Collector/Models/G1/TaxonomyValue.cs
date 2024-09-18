using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class TaxonomyValue
    {
        public string TermSetId { get; set; }
        public string TermGuid { get; set; }
        public string Label { get; set; }
        public int WssId { get; set; }
    }
}
