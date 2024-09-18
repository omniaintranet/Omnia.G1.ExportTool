using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Entities
{
    public class EntityBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        [Column(Order = 2)]
        public virtual Guid TenantId { get; set; }


        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        /// <value>
        /// The created by.
        /// </value>
        public string CreatedBy { get; set; }


        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        /// <value>
        /// The updated by.
        /// </value>
        public string UpdatedBy { get; set; }


        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        public virtual DateTimeOffset CreatedAt { get; set; }


        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        /// <value>
        /// The modified.
        /// </value>
        public DateTimeOffset ModifiedAt { get; set; }


        /// <summary>
        /// Gets or sets the deleted.
        /// </summary>
        /// <value>
        /// The deleted.
        /// </value>
        public DateTimeOffset? DeletedAt { get; set; }
    }
}
