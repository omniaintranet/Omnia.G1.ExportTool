using Omnia.MigrationG2.Foundation.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Configurations
{
    /// <summary>
    /// CopnfigurationConfiguration Class
    /// </summary>
    [Serializable]
    public class Configuration : ModelBase
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        /// <value>
        /// The region.
        /// </value>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the extension package identifier.
        /// </summary>
        /// <value>
        /// The extension package identifier.
        /// </value>
        public Guid ExtensionPackageId { get; set; }

        /// <summary>
        /// Gets or sets the permission role.
        /// </summary>
        /// <value>
        /// The permission role.
        /// </value>
        public string PermissionRole { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public dynamic Value { get; set; }

        /// <summary>
        /// IncludedInClient
        /// </summary>
        public bool IncludedInClient { get; set; }

        /// <summary>
        /// UIEditable
        /// </summary>
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
        /// Gets or sets a value indicating whether this instance is not allow update.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is not allow update; otherwise, <c>false</c>.
        /// </value>
        public bool AllowUpdateExistingValue { get; set; }

        /// <summary>
        /// The hash of the configuration object
        /// </summary>
        public string Hash { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            AllowUpdateExistingValue = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="name">The name of configuration.</param>
        /// <param name="region">The region for this configuration.</param>
        /// <param name="value">The configuration value.</param>
        //public Configuration(string name, string region, dynamic value)
        //{
        //    this.Name = name;
        //    this.Region = region;
        //    this.Value = value;
        //}

        /// <summary>
        /// Gets the configuration value.
        /// </summary>
        /// <returns></returns>
        public string GetValue()
        {
            return this.Value.ToString();
        }
    }

    public enum UITypes
    {
        String = 0,
        Boolean = 1
    }
}
