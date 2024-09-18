using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class NavigationProperties : ModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationProperties"/> class.
        /// </summary>
        public NavigationProperties()
        {
            this.Descriptions = new List<NavigationDescription>();
            this.Languages = new List<NavigationLanguage>();
        }

        public Guid TermStoreId { get; set; }

        public Guid TermSetId { get; set; }

        public string HoverText { get; set; }

        public string Title { get; set; }

        public bool IsTitleCustomized { get; set; }

        public bool IsPinned { get; set; }

        public Guid? PinSourceTermSetId { get; set; }

        public bool IsReused { get; set; }

        public Guid? SourceNodeId { get; set; }

        public Guid? SourceNodeTermSetId { get; set; }

        public ICollection<NavigationDescription> Descriptions { get; set; }

        public string TargetUrlForChildTerms { get; set; }

        public bool IsDraftNode { get; set; }

        public string OwnerNavigationTerm { get; set; }

        public string CustomLinkUrl { get; set; }

        public DateTime LastFriendlyUrlChangedAt { get; set; }

        public ICollection<NavigationLanguage> Languages { get; set; }

        public bool IsTargetUrlInherited { get; set; }

    }
}
