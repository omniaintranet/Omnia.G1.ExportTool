using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Entities.Tenants
{
    /// <summary>
    /// Tenant Class
    /// </summary>
    public class Tenant : EntityBase
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the authority URL.
        /// </summary>
        /// <value>
        /// The authority URL.
        /// </value>
        public string AuthorityUrl { get; set; }

        /// <summary>
        /// Gets or sets my site URL.
        /// </summary>
        /// <value>
        /// My site URL.
        /// </value>
        public string MySiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the admin URL.
        /// </summary>
        /// <value>
        /// The admin URL.
        /// </value>
        public string AdminUrl { get; set; }

        /// <summary>
        /// Gets or sets the application catalog URL.
        /// </summary>
        /// <value>
        /// The application catalog URL.
        /// </value>
        public string AppCatalogUrl { get; set; }

        /// <summary>
        /// This should not mapped here since its inherited from baseclass that all entitites will get
        /// but this is the Tenant class itself so we will not use TenantId here
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        [NotMapped]
        private new Guid TenantId { get; set; }
    }
}
