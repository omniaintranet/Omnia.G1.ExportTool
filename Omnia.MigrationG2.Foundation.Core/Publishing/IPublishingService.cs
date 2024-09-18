using Omnia.MigrationG2.Foundation.Core.SharePoint;
using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Publishing
{
    public interface IPublishingService
    {
        /// <summary>
        /// Gets the page list identifier.
        /// </summary>
        /// <param name="web">The web.</param>
        /// <returns></returns>
        Guid GetPageListId(ExtendedClientContext ctx, string webUrl, bool forceUpdateCache = false);
    }
}
