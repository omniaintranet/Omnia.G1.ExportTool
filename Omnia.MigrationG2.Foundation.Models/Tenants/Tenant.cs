using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Tenants
{
    [Serializable]
    public class Tenant
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the authority URL.
        /// </summary>
        /// <value>
        /// The authority URL.
        /// </value>
        [Required]
        public string AuthorityUrl { get; set; }

        /// <summary>
        /// Gets or sets my site URL.
        /// </summary>
        /// <value>
        /// My site URL.
        /// </value>
        [Required]
        public string MySiteUrl { get; set; }

        /// <summary>
        /// Gets or sets the admin URL.
        /// </summary>
        /// <value>
        /// The admin URL.
        /// </value>
        [Required]
        public string AdminUrl { get; set; }

        /// <summary>
        /// Gets or sets the application catalog URL.
        /// </summary>
        /// <value>
        /// The application catalog URL.
        /// </value>
        public string AppCatalogUrl { get; set; }
    }
}
