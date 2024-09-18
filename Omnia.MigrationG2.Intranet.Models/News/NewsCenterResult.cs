using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.News
{
    public class NewsCenterResult
    {
        public bool IsSiteNotFound { get; set; }
        public IEnumerable<NewsCenter> NewsCenters { get; set; }
    }
}
