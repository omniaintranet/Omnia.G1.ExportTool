using System;
using System.Collections.Generic;
using System.Text;
using static Omnia.MigrationG2.Collector.Models.G2.Enums;

namespace Omnia.MigrationG2.Collector.Models.G2
{
    public class PageData
    {
        public NavigationNodeType Type { get; set; }

        public Guid PageRendererId { get; set; } // ???

        public string Title { get; set; }

        public object Layout { get; set; } //Update for 6.x

        public Dictionary<string, object> EnterpriseProperties { get; set; }

        public PageData()
        {
            EnterpriseProperties = new Dictionary<string, object>();
        }
    }
}
