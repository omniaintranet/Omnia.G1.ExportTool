using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class PageCustomData
    {
        public List<LinkItem> Links { get; set; }

        public PageCustomData()
        {
            Links = new List<LinkItem>();
        }
    }
}
