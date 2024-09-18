using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class NavigationLanguage
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public bool IsDefault { get; set; }
        public int LCID { get; set; }

        public NavigationLanguage()
        {
            this.IsDefault = false;
        }
    }
}
