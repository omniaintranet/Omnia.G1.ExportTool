using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Collector.Models.G1
{
    public class G1SearchProperty : SearchProperty
    {
        public string displayNameInText { get; set; }
        public int refinerLimit { get; set; }
        public int refinerOrderBy { get; set; }
        public string widthType { get; set; }
        public bool isShowColumn { get; set; }
        public bool isShowRefiner { get; set; }
    }
}
