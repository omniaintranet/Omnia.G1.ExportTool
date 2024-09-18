using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Models.Navigations
{
    public class ClientCachedNavigationNode
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the support legacy webpart.
        /// </summary>
        /// <value>
        /// The support legacy webpart.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool? SupportLegacyWebpart { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<NavigationLabel> Labels { get; set; }

        /// <summary>
        /// Gets or sets the target page URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in global] navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in global]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowInGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in current] navigation.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in current]; otherwise, <c>false</c>.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowInCurrent { get; set; }

        /// <summary>
        /// Gets or sets the site URL.
        /// </summary>
        /// <value>
        /// The site URL.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        /// <value>
        /// The children.
        /// </value>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ClientCachedNavigationNode> Children { get; set; }

        // only available in the Root Node
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TermStoreId { get; set; }

        // only available in the Root Node
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TermSetId { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string HoverText { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsTitleCustomized { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDraftNode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CustomLinkUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string BackupFriendlyUrl { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Level { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool Selected { get; set; }
    }
}
