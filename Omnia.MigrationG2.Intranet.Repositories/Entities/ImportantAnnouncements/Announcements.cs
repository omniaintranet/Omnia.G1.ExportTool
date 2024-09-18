using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.ImportantAnnouncements
{
    public class Announcements : EntityBase
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key,Column(Order = 1)]
        //[Index("Navigation_Node", 2, IsClustered = false)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [support legacy webpart].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [support legacy webpart]; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 2)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 3)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 4)]
        public int Status { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 5)]
        public DateTimeOffset StartDate { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 6)]
        public DateTimeOffset EndDate { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 7)]
        public int Order { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 8)]
        public bool IsCloseDisabled { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 9)]
        public bool ForceToRedisplay { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 10)]
        public bool IsCommentsDisabled { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 11)]
        public Guid AnnouncementTypeId { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 12)]
        public Guid AnnouncementStatusTypeId { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 13)]
        public AnnouncementStatusTypes AnnouncementStatusTypes { get; set; }
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>
        /// The labels.
        /// </value>
        [Column(Order = 14)]
        public AnnouncementTypes AnnouncementTypes { get; set; }
    }
}
