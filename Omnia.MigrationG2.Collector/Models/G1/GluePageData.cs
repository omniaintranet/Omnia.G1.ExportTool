using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class GluePageData
    {
        public Guid? LayoutId { get; set; }
        public List<GluePageControl> Controls { get; set; }

        public GluePageData()
        {
            Controls = new List<GluePageControl>();
        }
    }
}
