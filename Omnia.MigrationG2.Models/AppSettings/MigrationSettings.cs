using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Models.AppSettings
{
    public class MigrationSettings
    {
        public bool CheckNavigationNodes { get; set; }

        public bool CheckNewsCenters { get; set; }

        public bool CheckCommonLinks { get; set; }

        public bool CheckMyLinks { get; set; }

        public bool CheckImportantAnnouncements { get; set; }

        public Guid TenantId { get; set; }

        public string NavigationSourceUrl { get; set; }
        public string NavigationSiteUrl { get; set; }
        public string SharePointUrl { get; set; }
        public string ParentId { get; set; }
        public string NewsCenterUrl { get; set; }

        public string Language { get; set; }
        public bool UseUserClientContext { get; set; }

        public bool UseSettingsInContentManagement { get; set; }

        public DateTime? PageMinDate { get; set; }

        public Dictionary<string, string> CustomPropertiesMappings { get; set; }

        public Dictionary<string, string> DefaultPropertiesMappings { get; set; }

        public MigrationSettings()
        {
            CustomPropertiesMappings = new Dictionary<string, string>();
            DefaultPropertiesMappings = new Dictionary<string, string>();
        }
    }
}
