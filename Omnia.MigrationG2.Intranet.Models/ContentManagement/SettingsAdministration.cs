using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class SettingsAdministration
    {
        public SettingsAdministration()
        {
            PageProperties = new List<PageProperty>();
            SelectedPageProperties = new List<PageProperty>();
        }

        public ICollection<PageProperty> PageProperties { get; set; }
        public ICollection<PageProperty> SelectedPageProperties { get; set; }
        public bool IsInheritParentSetting { get; set; }
        public string ParentUrl { get; set; }
    }
}
