using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.CommonLinks
{
    public class LinksItem : EntityBase
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
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [support legacy webpart].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [support legacy webpart]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 4), MaxLength(1024)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 5, TypeName = "nvarchar(max)")]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the properties.
        /// </summary>
        /// <value>
        /// The properties.
        /// </value>
        [Column(Order = 6, TypeName = "nvarchar(max)")]
        public string Information { get; set; }

        /// <summary>
        /// Gets or sets the friendly URL segment.
        /// </summary>
        /// <value>
        /// The friendly URL segment.
        /// </value>
        [Column(Order = 7)]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [Column(Order = 8)]
        public bool IsOpenNewWindow { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [Column(Order = 9)]
        public string LimitAccess { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>
        [Column(Order = 10)]
        public bool Mandatory { get; set; }

        /// <summary>
        /// Gets or sets the parent identifier.
        /// </summary>
        /// <value>
        /// The parent identifier.
        /// </value>

    }

}
