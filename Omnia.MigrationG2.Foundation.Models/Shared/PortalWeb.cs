using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Models.Shared
{
    public class PortalWeb
    {
        /// <summary>
        /// Gets or sets the server relative URL.
        /// </summary>
        /// <value>
        /// The server relative URL.
        /// </value>
        public string ServerRelativeUrl { get; set; }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the relative URL.
        /// </summary>
        /// <value>
        /// The relative URL.
        /// </value>
        public string RelativeUrl { get; set; }

        /// <summary>
        /// Gets or sets the web template.
        /// </summary>
        /// <value>
        /// The web template.
        /// </value>
        public string WebTemplate { get; set; }

        /// <summary>
        /// Gets or sets the editor group.
        /// </summary>
        /// <value>
        /// The editor group.
        /// </value>
        public string EditorGroup { get; set; }

        /// <summary>
        /// Gets or sets the lcid.
        /// </summary>
        /// <value>
        /// The lcid.
        /// </value>
        public int LCID { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is in recycle bin.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is in recycle bin; otherwise, <c>false</c>.
        /// </value>
        public bool IsInRecycleBin { get; set; }

        /// <summary>
        /// Gets or sets the recycle bin identifier.
        /// </summary>
        /// <value>
        /// The recycle bin identifier.
        /// </value>
        public Guid RecycleBinItemId { get; set; }

        /// <summary>
        /// Gets or sets the deleted status.
        /// </summary>
        /// <value>
        /// The deleted status.
        /// </value>
        public string DeletedStatus { get; set; }
    }
}
