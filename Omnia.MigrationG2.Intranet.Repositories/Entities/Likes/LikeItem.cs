using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.Likes
{
    public class LikeItem : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Column(Order = 1), MaxLength(1024)]
        //[Index("Navigation_Node", 2, IsClustered = false)]
        public string WebUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [support legacy webpart].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [support legacy webpart]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 2)]
        public int PageId { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Key,Column(Order = 3)]
        public string CommentId { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 4)]
        public string TopicId { get; set; }
    }
}
