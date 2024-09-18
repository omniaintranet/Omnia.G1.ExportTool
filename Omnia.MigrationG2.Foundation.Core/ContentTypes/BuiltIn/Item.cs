using Omnia.MigrationG2.Foundation.Core.Fields;
using Omnia.MigrationG2.Foundation.Core.Fields.BuiltIn;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes.BuiltIn
{
    /// <summary>
    /// The item.
    /// </summary>
    [ContentType(id: "0x01", name: "Item", isBuiltIn: true, Group = "List Content Types", Description = "Create a new list item")]
    public class Item : ContentTypeBase
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets the created.
        /// </summary>
        [FieldRef(typeof(Created))]
        public virtual DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [FieldRef(typeof(Author))]
        public virtual string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the edited by.
        /// </summary>
        [FieldRef(typeof(Editor))]
        public virtual string EditedBy { get; set; }

        /// <summary>
        /// Gets or sets the list item id.
        /// </summary>
        public int ListItemId { get; set; }

        /// <summary>
        /// Gets or sets the list item unique id.
        /// </summary>
        public Guid ListItemUniqueId { get; set; }

        /// <summary>
        /// Gets or sets the modified.
        /// </summary>
        [FieldRef(typeof(Modified))]
        public virtual DateTime Modified { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [FieldRef(typeof(Title))]
        public virtual string Title { get; set; }

        #endregion
    }
}
