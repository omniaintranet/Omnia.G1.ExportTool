using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Repositories.Entities.OmniaProfiles
{
    /// <summary>
    /// Log Class
    /// </summary>
    public class OmniaProfile : EntityBase
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
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [MaxLength(100)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        //[Index("Omnia_Profile_Clustered", 1, IsClustered = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClustedId { get; set; }
    }
}
