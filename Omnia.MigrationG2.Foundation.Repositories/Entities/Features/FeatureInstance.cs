using Omnia.MigrationG2.Foundation.Models.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Entities.Features
{
    /// <summary>
    /// Feature
    /// </summary>
    public class FeatureInstance : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key, Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        [Column(Order = 2)]
        //[Index("Tenant_Feature", 1, IsClustered = false, IsUnique = true)]
        public override Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the feature identifier.
        /// </summary>
        /// <value>
        /// The feature identifier.
        /// </value>
        //[Index("Tenant_Feature", 2, IsClustered = false, IsUnique = true)]
        public Guid FeatureId { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target site.
        /// </value>
        //[Index("Tenant_Feature", 3, IsClustered = false, IsUnique = true)]
        [MaxLength(2083)]
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        public int Step { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public FeatureInstanceStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the application feature.
        /// </summary>
        /// <value>
        /// The application feature.
        /// </value>
        //public virtual Feature Feature { get; set; }


    }
}
