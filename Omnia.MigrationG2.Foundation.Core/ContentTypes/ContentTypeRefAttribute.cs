using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes
{
    /// <summary>
    /// The content type ref attribute.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ContentTypeRefAttribute : Attribute
    {
        private string _id;
        private ContentTypeBase _contentType;

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeRefAttribute"/> class. 
        /// </summary>
        /// <param name="id">The identifier.</param>
        public ContentTypeRefAttribute(string id)
        {
            // From bad stuff from guid {c5760ec5-13c9-4ab1-ae1d-74aa68ee1ef4}
            this._id = id.Replace("-", "").Replace("{", string.Empty).Replace("}", string.Empty);
            this.IsBuiltIn = true;
        }

        public ContentTypeRefAttribute(Type type, bool isDefault = false)
        {
            if (type.IsSubclassOf(typeof(ContentTypeBase)))
            {
                _contentType = Activator.CreateInstance(type) as ContentTypeBase;
                if (_contentType != null)
                {
                    this._id = _contentType.IdAsString;
                    this.Name = _contentType.Name;
                    this.IsBuiltIn = _contentType.IsBuiltIn;
                    _contentType.IsDefault = isDefault;
                }
            }
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id
        {
            get { return this._id; }
        }

        /// <summary>
        /// Gets or sets the name of the content type.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is built in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is built in; otherwise, <c>false</c>.
        /// </value>
        public bool IsBuiltIn { get; set; }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>
        /// The type of the content.
        /// </value>
        public ContentTypeBase ContentType
        {
            get { return this._contentType; }
        }

        #endregion
    }
}
