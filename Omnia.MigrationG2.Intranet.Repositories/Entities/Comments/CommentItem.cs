using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.Comments
{
    public class CommentItem : EntityBase
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
        public string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [support legacy webpart].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [support legacy webpart]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 4)]
        public int PageId { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 5, TypeName = "nvarchar(max)")]
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [Column(Order = 6)]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the friendly URL segment.
        /// </summary>
        /// <value>
        /// The friendly URL segment.
        /// </value>
        [Column(Order = 7)]
        public bool isDelete { get; set; }

        /// <summary>
        /// Gets or sets the custom sort order.
        /// </summary>
        /// <value>
        /// The custom sort order.
        /// </value>
        [Column(Order = 8)]
        public string TopicId { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
    }
}
