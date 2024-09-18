using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Comments;
using Omnia.MigrationG2.Intranet.Models.ContentManagement;
using Omnia.MigrationG2.Intranet.Models.Likes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class NavigationNode : INavigationNode<NavigationNode>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationNode"/> class.
        /// </summary>
        public NavigationNode()
        {
            this.Children = new List<NavigationNode>();
            this.Labels = new List<NavigationLabel>();
            this.Descriptions = new List<NavigationDescription>();
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the support legacy webpart.
        /// </summary>
        /// <value>
        /// The support legacy webpart.
        /// </value>
        public bool? SupportLegacyWebpart { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        public ICollection<NavigationLabel> Labels { get; set; }

        /// <summary>
        /// Gets or sets the friendly URL segment.
        /// </summary>
        /// <value>
        /// The friendly URL segment.
        /// </value>
        public string FriendlyUrlSegment { get; set; }

        /// <summary>
        /// Gets or sets the custom sort order.
        /// </summary>
        /// <value>
        /// The custom sort order.
        /// </value>
        public string CustomSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        public Guid ParentId { get; set; }

        /// <summary>
        /// Gets or sets the target page URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in global] navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in global]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowInGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in current] navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in current]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowInCurrent { get; set; }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NavigationNode"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        public List<NavigationNode> Children { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        [JsonIgnore]
        public NavigationNode Parent { get; set; }

        [JsonIgnore]
        public NavigationProperties Properties
        {
            get
            {
                return BuildProperties();
            }
            set
            {
                SetPropertiesValue(value);
            }
        }

        [JsonIgnore]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonIgnore]
        public bool IsPinned { get; set; }

        [JsonIgnore]
        public Guid? PinSourceTermSetId { get; set; }

        [JsonIgnore]
        public bool IsReused { get; set; }

        [JsonIgnore]
        public Guid? SourceNodeId { get; set; }

        [JsonIgnore]
        public Guid? SourceNodeTermSetId { get; set; }

        [JsonIgnore]
        public DateTime LastModifiedDate { get; set; }

        [JsonIgnore]
        public bool ForceChildrenUseDelayUrl { get; set; }


        public Guid TermStoreId { get; set; }

        public Guid TermSetId { get; set; }

        public string HoverText { get; set; }

        public string Title { get; set; }

        public bool IsTitleCustomized { get; set; }

        public ICollection<NavigationDescription> Descriptions { get; set; }

        public string TargetUrlForChildTerms { get; set; }

        public bool IsDraftNode { get; set; }

        public string OwnerNavigationTerm { get; set; }

        public string CustomLinkUrl { get; set; }

        public string Url { get; set; }

        public string BackupFriendlyUrl { get; set; }

        public bool IsTargetUrlInherited { get; set; }

        public int Level { get; set; }

        public bool IsDuplicateTargetUrl { get; set; }

        public bool Selected { get; set; }

        public List<NavigationNode> TranslationPages { get; set; }
        public List<CommentItem> Comments { get; set; }
        public List<LikeItem> Likes { get; set; }
        public Guid ListId { get; set; }
        // MigrationG2 props
        public PageInfo PageInfo { get; set; }
        public PageInfoPropertiesSettings PageInfoPropertiesSettings { get; set; }

        private void SetPropertiesValue(NavigationProperties properties)
        {
            TermStoreId = properties.TermStoreId;
            TermSetId = properties.TermSetId;
            HoverText = properties.HoverText;

            Title = properties.Title;
            IsTitleCustomized = properties.IsTitleCustomized;
            IsPinned = properties.IsPinned;
            PinSourceTermSetId = properties.PinSourceTermSetId;
            IsReused = properties.IsReused;
            SourceNodeId = properties.SourceNodeId;
            SourceNodeTermSetId = properties.SourceNodeTermSetId;
            if (properties.Descriptions != null)
                Descriptions = properties.Descriptions;

            TargetUrlForChildTerms = properties.TargetUrlForChildTerms;
            IsDraftNode = properties.IsDraftNode;
            OwnerNavigationTerm = properties.OwnerNavigationTerm;
            CustomLinkUrl = properties.CustomLinkUrl;
            LastModifiedDate = properties.LastFriendlyUrlChangedAt;

            IsTargetUrlInherited = properties.IsTargetUrlInherited;
        }

        private NavigationProperties BuildProperties()
        {
            var properties = new NavigationProperties();
            properties.TermStoreId = TermStoreId;
            properties.TermSetId = TermSetId;
            properties.HoverText = HoverText;

            properties.Title = Title;
            properties.IsTitleCustomized = IsTitleCustomized;
            properties.IsPinned = IsPinned;
            properties.PinSourceTermSetId = PinSourceTermSetId;
            properties.IsReused = IsReused;
            properties.SourceNodeId = SourceNodeId;
            properties.SourceNodeTermSetId = SourceNodeTermSetId;
            properties.Descriptions = Descriptions;
            properties.TargetUrlForChildTerms = TargetUrlForChildTerms;
            properties.IsDraftNode = IsDraftNode;
            properties.OwnerNavigationTerm = OwnerNavigationTerm;
            properties.CustomLinkUrl = CustomLinkUrl;
            properties.LastFriendlyUrlChangedAt = LastModifiedDate;

            properties.IsTargetUrlInherited = IsTargetUrlInherited;

            return properties;
        }

    }
}
