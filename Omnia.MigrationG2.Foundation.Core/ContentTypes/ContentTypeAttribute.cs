using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.ContentTypes
{
    /// <summary>
    /// The content type attribute. Multiuse of attribute false.
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContentTypeAttribute : ContentTypeRefAttribute
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentTypeAttribute" /> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="isBuiltIn">if set to <c>true</c> [is built in].</param>
        public ContentTypeAttribute(string id, string name, bool isBuiltIn = false)
            : base(id)
        {
            this.Name = name;
            this.IsBuiltIn = isBuiltIn;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets whether the content type can be modified.
        /// </summary>
        /// 
        /// <returns>
        /// true if the content type is sealed; otherwise false.
        /// </returns>
        public bool Sealed { get; set; }

        /// <summary>
        /// Gets or sets the name of a custom Display form template to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string DisplayFormTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the name of a custom Edit form template to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string EditFormTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the name of a custom New form template to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string NewFormTemplateName { get; set; }

        /// <summary>
        /// Gets or set the URL of a custom New form page to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns a <see cref="T:System.String"/> object.
        /// </returns>
        public string NewFormUrl { get; set; }

        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string MobileNewFormUrl { get; set; }
        /// <summary>
        /// Gets or set the URL of a custom Edit form page to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string EditFormUrl { get; set; }

        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string MobileEditFormUrl { get; set; }

        /// <summary>
        /// Gets or set the URL of a custom Display form page to use for items assigned this content type.
        /// </summary>
        /// 
        /// <returns>
        /// Returns a <see cref="T:System.String"/> object.
        /// </returns>
        public string DisplayFormUrl { get; set; }

        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string MobileDisplayFormUrl { get; set; }

        /// <summary>
        /// Gets or sets whether the content type is read-only.
        /// </summary>
        /// <returns>
        /// true if the content type is read-only; otherwise false.
        /// </returns>
        public bool ReadOnly { get; set; }

        //public Guid FeatureId { get; internal set; }
        /// <summary>
        /// Gets or sets a string that contains a description of the content type.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> that contains text describing the content type. The default value is String.Empty.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException">You are not permitted to set the property to a null value.</exception>
        public string Description { get; set; }

        public string JSLink { get; set; }

        /// <summary>
        /// Gets or sets whether the content type is hidden on the list’s New menu.
        /// </summary>
        /// 
        /// <returns>
        /// true if the content type is hidden on the list’s New menu; otherwise false.
        /// </returns>
        /// <exception cref="T:Microsoft.Sharepoint.SPContentTypeReadOnlyException">The value of the <see cref="P:Microsoft.SharePoint.SPContentType.ReadOnly"/> property is true.</exception><exception cref="T:Microsoft.Sharepoint.SPContentTypeSealedException">The value of the <see cref="P:Microsoft.SharePoint.SPContentType.Sealed"/> property is true.</exception>
        public bool Hidden { get; set; }

        /// <summary>
        /// Gets and sets the content type group to which the content type is assigned.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> containing the name of a group.
        /// </returns>
        public string Group { get; set; }


        /// <summary>
        /// Gets or sets the document template for the content type.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> containing a path to the template.
        /// </returns>
        public string DocumentTemplate { get; set; }

        /// <summary>
        /// Gets or sets a string that identifies the application used to create new documents.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.String"/> object that contains the programmatic identifier of the application. The default value is String.Empty.
        /// </returns>
        public string NewDocumentControl { get; set; }

        /// <summary>
        /// Indicates whether the client is responsible for rendering a new document when it is created.
        /// </summary>
        /// 
        /// <returns>
        /// True if new documents are rendered on the client, otherwise; false. The default is true.
        /// </returns>
        public bool RequireClientRenderingOnNew { get; set; }

        /// <returns>
        /// Returns <see cref="T:System.String"/>.
        /// </returns>
        public string SchemaXmlWithResourceTokens { get; set; }

        #endregion
    }
}
