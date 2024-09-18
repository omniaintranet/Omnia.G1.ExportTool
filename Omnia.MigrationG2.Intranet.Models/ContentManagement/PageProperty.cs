using Newtonsoft.Json;
using Omnia.MigrationG2.Foundation.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.ContentManagement
{
    public class PageProperty
    {
        public PageProperty()
        {
            AdditionalInfo = new Dictionary<string, string>();
        }

        public string DisplayName { get; set; }
        public string InternalName { get; set; }
        public string TypeAsString { get; set; }
        public IDictionary<string, string> AdditionalInfo { get; set; }
        public bool IsSelected { get; set; }
        public bool IsShowInEditMode { get; set; }
        public bool IsShowInViewMode { get; set; }
        public bool IsShowInShowMore { get; set; }
        public bool IsShared { get; set; }
        public bool IsShowLabel { get; set; }
        public bool ReadOnlyField { get; set; }
        public bool Required { get; set; }

        [JsonIgnore]
        public IEnumerable<PeoplePickerPrincipal> GroupPermission { get; set; }

        public bool CanEditField { get; set; }
        public int DisplayType { get; set; }
        public bool IsTargetingField { get; set; }

        //DateOnly or DateTime
        public string DisplayFormat { get; set; }

        // MigrationG2 Properties
        [JsonIgnore]
        public bool IsMigrationG2Property { get; set; }
    }
}
