using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.News
{
    public class NewsCenterFilter
    {
        public string NewsCenterUrl { get; set; }
        public ICollection<QueryFilter> Filters { get; set; }
        //public bool UseTargetingSettings { get; set; }
    }
}
