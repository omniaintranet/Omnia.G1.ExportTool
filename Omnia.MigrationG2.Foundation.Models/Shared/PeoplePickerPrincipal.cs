using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Shared
{
    public class PeoplePickerPrincipal
    {
        public PeoplePickerPrincipal()
        {
            EntityData = new Dictionary<string, string>();
        }

        public string Key { get; set; }
        public string Description { get; set; }
        public string DisplayText { get; set; }
        public string EntityType { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderName { get; set; }
        public bool IsResolved { get; set; }
        public Dictionary<string, string> EntityData { get; set; }
        public dynamic MultipleMatches { get; set; }
        public bool IsUnauthenticatedExternalEmail { get; set; }
    }
}
