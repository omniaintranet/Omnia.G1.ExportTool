using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class NavigationTopNode : ModelBase
    {
        public ClientCachedNavigationNode Node { get; set; }
        public HashNavigationNode HashNode { get; set; }
    }
}
