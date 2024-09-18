using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.Navigations
{
    public class NavigationNode : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, Column(Order = 1)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        [Column(Order = 2)]
        //[Index("Navigation_Node", 1, IsClustered = false)]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the navigation source URL.
        /// </summary>
        /// <value>
        /// The navigation source URL.
        /// </value>
        [Column(Order = 3), MaxLength(1024)]
        //[Index("Navigation_Node", 2, IsClustered = false)]
        public string NavigationSourceUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [support legacy webpart].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [support legacy webpart]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 4)]
        public bool SupportLegacyWebpart { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 5, TypeName = "nvarchar(max)")]
        [Required]
        public string Labels { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [Column(Order = 6, TypeName = "nvarchar(max)")]
        [Required]
        public string Properties { get; set; }

        /// <summary>
        /// Gets or sets the friendly URL segment.
        /// </summary>
        /// <value>
        /// The friendly URL segment.
        /// </value>
        [Column(Order = 7), MaxLength(1024)]
        public string FriendlyUrlSegment { get; set; }

        /// <summary>
        /// Gets or sets the custom sort order.
        /// </summary>
        /// <value>
        /// The custom sort order.
        /// </value>
        [Column(Order = 8, TypeName = "nvarchar(max)")]
        public string CustomSortOrder { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [Column(Order = 9)]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the target URL.
        /// </summary>
        /// <value>
        /// The target URL.
        /// </value>
        [Column(Order = 10), MaxLength(1024)]
        public string TargetUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in global].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in global]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 11)]
        public bool ShowInGlobal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in current].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in current]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 12)]
        public bool ShowInCurrent { get; set; }

        /// <summary>
        /// Gets or sets the site target URL.
        /// </summary>
        /// <value>
        /// The site target URL.
        /// </value>
        [Column(Order = 13), MaxLength(1024)]
        public string SiteUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="NavigationNode"/> is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if deleted; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 14)]
        //[Index("Navigation_Node", 3, IsClustered = false)]
        public bool Deleted { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        [Column(Order = 15)]
        //[Index("Navigation_Node", 4, IsClustered = false)]
        public new DateTimeOffset ModifiedAt { get; set; }

        /// <summary>
        /// Gets or sets the synced at.
        /// </summary>
        /// <value>
        /// The synced at.
        /// </value>
        [Column(Order = 16)]
        //[Index("Navigation_Node", 5, IsClustered = false)]
        public DateTimeOffset? SyncedAt { get; set; }

        [Column(Order = 17)]
        //[Index("Navigation_Node_Clustered", 1, IsClustered = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClustedId { get; set; }

    }
}
