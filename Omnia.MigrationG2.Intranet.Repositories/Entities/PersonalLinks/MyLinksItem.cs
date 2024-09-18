using Newtonsoft.Json;
using Omnia.MigrationG2.Intranet.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Repositories.Entities.PersonalLinks
{
    public class MyLinksItem : EntityBase
    {
        /// <summary>
        /// Gets or sets the tenant identifier.
        /// </summary>
        /// <value>
        /// The tenant identifier.
        /// </value>
        [Key, Column(Order = 1)]
        public new Guid TenantId { get; set; }

        /// <summary>
        /// Gets or sets the link identifier.
        /// </summary>
        /// <value>
        /// The link identifier.
        /// </value>
        [Key, Column(Order = 2)]
        public new Guid LinkId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user login.
        /// </summary>
        /// <value>
        /// The name of the user login.
        /// </value>
        [Key, Column(Order = 3)]
        public string UserLoginName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        [MaxLength(255), Column(Order = 4)] 
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [Column(Order = 5)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        [MaxLength(255), Column(Order = 6)]
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        [Column(Order = 7)]
        public string Information { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        [Column(Order = 8)]
        public string Icon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is open new window.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is open new window; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 9)]
        public bool IsOpenNewWindow { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is owner.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is owner; otherwise, <c>false</c>.
        /// </value>
        [Column(Order = 10)]
        public bool IsOwner { get; set; }

    }

}
