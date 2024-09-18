using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class PageInfoPropertiesSettings
    {
        public List<PageProperty> PagePropertiesSetting { get; set; }
        public Dictionary<string, object> Properties { get; set; }
        public List<string> BooleanFields { get; set; }
        //public TargetingDefinition TargetingDefinition { get; set; }
    }
}
