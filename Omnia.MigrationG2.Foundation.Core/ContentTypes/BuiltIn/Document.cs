using Omnia.MigrationG2.Foundation.Core.Fields;
using Omnia.MigrationG2.Foundation.Core.Fields.BuiltIn;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes.BuiltIn
{
    /// <summary>
    /// The document.
    /// </summary>
    [ContentType(id: "0x0101", name: "Document", isBuiltIn: true, Group = "Document Content Types", Description = "Create a new document")]
    public class Document : Item
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [FieldRef(typeof(FileLeafRef))]
        public string Name { get; set; }

        #endregion
    }
}
