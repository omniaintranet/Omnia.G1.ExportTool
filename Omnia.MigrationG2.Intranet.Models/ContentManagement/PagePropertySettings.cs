using Omnia.MigrationG2.Foundation.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class PagePropertySettings
    {
        public PagePropertySettings()
        {
            GroupPermission = new List<PeoplePickerPrincipal>();
            IsMigrationG2Property = false;
        }
        public string InternalName { get; set; }
        public bool IsShowInEditMode { get; set; }
        public bool IsShowInViewMode { get; set; }
        public bool IsShowInShowMore { get; set; }
        public bool IsShared { get; set; }
        public bool IsShowLabel { get; set; }
        public bool Required { get; set; }
        public int DisplayType { get; set; }
        public IEnumerable<PeoplePickerPrincipal> GroupPermission { get; set; }

        // MigrationG2 Properties
        public bool IsMigrationG2Property { get; set; }
    }
}
