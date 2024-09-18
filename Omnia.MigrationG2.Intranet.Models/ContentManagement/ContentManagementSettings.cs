using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class ContentManagementSettings
    {
        public ContentManagementSettings()
        {
            AvailablePageProperties = new List<PagePropertySettings>();
        }

        public List<PagePropertySettings> AvailablePageProperties { get; set; }
        public bool IsInheritParentSetting { get; set; }
    }
}
