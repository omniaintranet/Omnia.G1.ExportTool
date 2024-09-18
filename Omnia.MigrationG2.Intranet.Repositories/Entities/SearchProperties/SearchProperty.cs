using Omnia.MigrationG2.Intranet.Models.SearchProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.SearchProperties
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchProperty : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the managed property.
        /// </summary>
        /// <value>
        /// The managed property.
        /// </value>
        public string ManagedProperty { get; set; }

        /// <summary>
        /// Gets or sets the managed retrieve property.
        /// </summary>
        /// <value>
        /// The managed retrieve property.
        /// </value>
        public string ManagedRetrieveProperty { get; set; }

        /// <summary>
        /// Gets or sets the managed refiner property.
        /// </summary>
        /// <value>
        /// The managed refiner property.
        /// </value>
        public string ManagedRefinerProperty { get; set; }

        /// <summary>
        /// Gets or sets the managed query property.
        /// </summary>
        /// <value>
        /// The managed query property.
        /// </value>
        public string ManagedQueryProperty { get; set; }

        /// <summary>
        /// Gets or sets the managed sort property.
        /// </summary>
        /// <value>
        /// The managed sort property.
        /// </value>
        public string ManagedSortProperty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is title property.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is title property; otherwise, <c>false</c>.
        /// </value>
        public bool IsTitleProperty { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SearchProperty"/> is retrievable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if retrievable; otherwise, <c>false</c>.
        /// </value>
        public bool Retrievable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SearchProperty"/> is refinable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if refinable; otherwise, <c>false</c>.
        /// </value>
        public bool Refinable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SearchProperty"/> is queryable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if queryable; otherwise, <c>false</c>.
        /// </value>
        public bool Queryable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SearchProperty"/> is sortable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if sortable; otherwise, <c>false</c>.
        /// </value>
        public bool Sortable { get; set; }

        /// <summary>
        /// Gets or sets the formatting.
        /// </summary>
        /// <value>
        /// The formatting.
        /// </value>
        public SearchPropertyFormatting Formatting { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public SearchPropertyCategory Category { get; set; }
    }
}
