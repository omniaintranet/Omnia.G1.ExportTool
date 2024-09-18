using Omnia.MigrationG2.Foundation.Core.ContentTypes;
using Omnia.MigrationG2.Foundation.Core.Fields;
using Omnia.MigrationG2.Foundation.Core.Fields.BuiltIn;
using Omnia.MigrationG2.Intranet.Core.ContentTypes.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Intranet.Core.ContentTypes.News
{
    [ContentType(id: "01E5AAD9-420B-4C13-8677-506D06E1AA34", name: "$Localize:OMI.News.ContentType.NewsArticle.Name;",
        Group = Foundation.Core.Constants.Common.ContentTypeGroup, Description = "$Localize:OMI.News.ContentType.NewsArticle.Description;")]
    public class OmniaNewsArticlePage : OmniaArticlePage
    {
        /// <summary>
        /// Gets or sets the enterprise keyword.
        /// </summary>
        /// <value>
        /// The enterprise keyword.
        /// </value>
        [FieldRef(typeof(EnterpriseKeyword))]
        public string EnterpriseKeyword { get; set; }
    }
}
