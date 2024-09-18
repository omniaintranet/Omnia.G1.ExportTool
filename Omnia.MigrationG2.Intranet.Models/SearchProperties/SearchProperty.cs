using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.SearchProperties
{
    public class SearchProperty
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string ManagedProperty { get; set; }
        public string ManagedRetrieveProperty { get; set; }
        public string ManagedRefinerProperty { get; set; }
        public string ManagedQueryProperty { get; set; }
        public string ManagedSortProperty { get; set; }
        public bool IsTitleProperty { get; set; }
        public Guid TenantId { get; set; }

        public bool Retrievable { get; set; }
        public bool Refinable { get; set; }
        public bool Queryable { get; set; }
        public bool Sortable { get; set; }
        public SearchPropertyFormatting Formatting { get; set; }
        public SearchPropertyCategory Category { get; set; }
    }
}
