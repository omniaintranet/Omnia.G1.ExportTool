using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class HashNavigationNode : ModelBase
    {
        public string Key { get; set; }
        public string Hash { get; set; }

        //This property use for sync between multi request on same navigation in cache
        //[ScriptIgnore]
        //public bool IsDeleted { get; set; }
    }
}
