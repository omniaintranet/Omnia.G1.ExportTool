using Omnia.MigrationG2.Foundation.Core.ContentTypes;
using Omnia.MigrationG2.Foundation.Core.ContentTypes.BuiltIn;
using Omnia.MigrationG2.Foundation.Core.Fields;
using Omnia.MigrationG2.Intranet.Core.Fields;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core.ContentTypes.Core
{
    /// <summary>
    /// PFPArticlePage content type
    /// </summary>
    /// <seealso cref="Omnia.Foundation.Extensibility.ContentTypes.BuiltIn.ArticlePage" />
    [ContentType(id: "FE768BAF-0348-4F5F-8C73-3C43EC34A438", name: "$Localize:OMI.Core.ContentType.ArticlePage.Name;",
        Group = Foundation.Core.Constants.Common.ContentTypeGroup, Description = "$Localize:OMI.Core.ContentType.ArticlePage.Description;")]
    public class OmniaArticlePage : ArticlePage
    {
        /// <summary>
        /// Gets or sets the custom data field.
        /// </summary>
        /// <value>
        /// PFPCustomDataField.
        /// </value>
        [FieldRef(typeof(OmniaCustomDataField))]
        public string OmniaCustomDataField { get; set; }

        /// <summary>
        /// Gets or sets the draft navigation term field.
        /// </summary>
        /// <value>
        /// The PFP draft navigation term field.
        /// </value>
        [FieldRef(typeof(OmniaDraftNavigationTermField))]
        public string OmniaDraftNavigationTermField { get; set; }

        /// <summary>
        /// Gets or sets the omnia review date field.
        /// </summary>
        /// <value>
        /// The omnia review date field.
        /// </value>
        [FieldRef(typeof(OmniaReviewDateField))]
        public string OmniaReviewDateField { get; set; }

        /// <summary>
        /// Gets or sets the omnia language field.
        /// </summary>
        /// <value>
        /// The omnia language field.
        /// </value>
        [FieldRef(typeof(OmniaLanguageField))]
        public string OmniaLanguageField { get; set; }

        /// <summary>
        /// Gets or sets the omnia source language field.
        /// </summary>
        /// <value>
        /// The omnia source language field.
        /// </value>
        [FieldRef(typeof(OmniaSourceLanguageField))]
        public string OmniaSourceLanguageField { get; set; }

        /// <summary>
        /// Gets or sets the omnia target languages field.
        /// </summary>
        /// <value>
        /// The omnia target languages field.
        /// </value>
        [FieldRef(typeof(OmniaTargetLanguagesField))]
        public string OmniaTargetLanguagesField { get; set; }

        /// <summary>
        /// Gets or sets the omnia glue data field.
        /// </summary>
        /// <value>
        /// The omnia glue data field.
        /// </value>
        [FieldRef(typeof(OmniaGlueDataField))]
        public string OmniaGlueDataField { get; set; }

        /// <summary>
        /// Gets or sets the omnia publishing start date.
        /// </summary>
        /// <value>
        /// The omnia publishing start date.
        /// </value>
        [FieldRef(typeof(OmniaPublishingStartDate))]
        public string OmniaPublishingStartDate { get; set; }

        /// <summary>
        /// Gets or sets the omnia publishing end date.
        /// </summary>
        /// <value>
        /// The omnia publishing end date.
        /// </value>
        [FieldRef(typeof(OmniaPublishingEndDate))]
        public string OmniaPublishingEndDate { get; set; }
    }
}
