using Omnia.MigrationG2.Foundation.Models.Configurations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Entities.Configurations
{
    /// <summary>
    /// CopnfigurationConfiguration Class
    /// </summary>
    public class Configuration : EntityBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        /// Here we override the base tenantId since we use it as a identifier with Key
        [Column(Order = 1)]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Column(Order = 2), MaxLength(2048)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        [Column(Order = 3)]
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the extension package identifier.
        /// </summary>
        /// <value>
        /// The extension package identifier.
        /// </value>
        [Column(Order = 4)]
        public Guid ExtensionPackageId { get; set; }

        /// <summary>
        /// Gets or sets the permission roles.
        /// </summary>
        /// <value>
        /// The permission roles.
        /// </value>
        public string PermissionRole { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the included in client flag.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public bool IncludedInClient { get; set; }

        /// <summary>
        /// Gets or sets the UI editable flag.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public bool UIEditable { get; set; }

        /// <summary>
        /// Gets or sets the UI data type.
        /// </summary>
        public UITypes UIType { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// The hash of the configuration object
        /// </summary>
        public string Hash { get; set; }
    }
}
