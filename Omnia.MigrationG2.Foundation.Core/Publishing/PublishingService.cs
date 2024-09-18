using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint.Client;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Foundation.Core.Caching;
using Omnia.MigrationG2.Foundation.Core.SharePoint;

namespace Omnia.MigrationG2.Foundation.Core.Publishing
{
    public class PublishingService : IPublishingService
    {
        private const string PublishingPageListIdProperty = "__PagesListId";

        private ICacheService _cacheService;
        public ICacheService Cache
        {
            get
            {
                if (_cacheService.IsNull())
                {
                    _cacheService = AppUtils.GetService<ICacheService>();
                }
                return _cacheService;
            }
        }

        public Guid GetPageListId(ExtendedClientContext ctx, string webUrl, bool forceUpdateCache = false)
        {
            var pageListIdCacheKey = GetCacheKeyForPageListId(webUrl);
            var result = Cache.GetFromMemoryCache<Guid?>(pageListIdCacheKey);

            if (result == null || forceUpdateCache)
            {
                ctx.Load(ctx.Web, w => w.AllProperties);
                ctx.ExecuteQuery();
                result = Guid.Empty;
                if (ctx.Web.AllProperties.FieldValues.ContainsKey(PublishingPageListIdProperty))
                {
                    result = new Guid(ctx.Web.AllProperties[PublishingPageListIdProperty].ToString());
                    Cache.AddOrUpdateMemoryCache(pageListIdCacheKey, result, DateTime.UtcNow.AddDays(1));
                }
            }
            return result.Value;
        }

        /// <summary>
        /// Gets the cache key for page list identifier.
        /// </summary>
        /// <param name="webUrl">The web URL.</param>
        /// <returns></returns>
        private string GetCacheKeyForPageListId(string webUrl)
        {
            return string.Format("{0}-{1}", Core.Constants.Caching.PageListId, webUrl);
        }
    }
}
