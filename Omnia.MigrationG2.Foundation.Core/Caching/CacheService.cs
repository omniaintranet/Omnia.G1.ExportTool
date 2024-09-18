using Microsoft.Extensions.Options;
using Omnia.MigrationG2.Core;
using Omnia.MigrationG2.Models.AppSettings;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Caching
{
    public class CacheService : ICacheService
    {
        private static Guid _tenantId;

        private Guid TenantId
        {
            get
            {
                if (_tenantId == Guid.Empty)
                {
                    _tenantId = AppUtils.MigrationSettings.TenantId;
                }

                return _tenantId;
            }
        }

        private static ConcurrentDictionary<string, string> _regionMappings;

        public CacheService()
        {
            _regionMappings = new ConcurrentDictionary<string, string>();
        }

        /// <summary>
        /// Gets item from the default MemoryCache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="cacheKey">The cache key.</param>
        /// <returns></returns>
        public T GetFromMemoryCache<T>(string key, string region = "")
        {
            region = region.ToLower();
            key = GetCacheKey(key, region);
            var item = MemoryCache.Default.GetCacheItem(key);
            if (item == null)
            {
                return default(T);
            }

            if (typeof(T) != typeof(CachedItem) && item.Value.IsNotNull() && item.Value is CachedItem)
            {
                return (T)((item.Value as CachedItem).Value);
            }

            return (T)item.Value;
        }

        /// <summary>
        /// Gets the cache key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        private string GetCacheKey(string key, string region)
        {
            key = key.ToLower();
            region = region.ToLower();
            return TenantId + FormatRegion(region) + key;
        }

        /// <summary>
        /// Formats the region.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns></returns>
        private string FormatRegion(string region)
        {
            if (region.IsNotNull())
            {
                region = $"[{region.ToLower()}]";
            }
            return region;
        }

        /// <summary>
        /// Adds object to the default MemoryCache with expiry DateTimeOffset
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        public void AddOrUpdateMemoryCache(string key, object value, DateTimeOffset? expires, string region = "")
        {
            AddOrUpdateMemoryCacheInternal(key, value, expires, region);
        }

        /// <summary>
        /// Adds object to the default MemoryCache. The object will be cached until the appdomain ends
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void AddOrUpdateMemoryCache(string key, object value, string region = "")
        {
            AddOrUpdateMemoryCacheInternal(key, value, null, region);
        }

        /// <summary>
        /// Adds the or update memory cache internal.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="expires">The expires.</param>
        /// <param name="region">The region.</param>
        private void AddOrUpdateMemoryCacheInternal(string key, object value, DateTimeOffset? expires, string region)
        {
            var validExpires = EnsureExpiredTimeValid(expires);
            region = region.ToLower();
            key = key.ToLower();
            key = GetCacheKey(key, region);

            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = validExpires,
                RemovedCallback = (argument) =>
                {
                    string removedItem = null;
                    if (_regionMappings.ContainsKey(argument.CacheItem.Key))
                    {
                        _regionMappings.TryRemove(argument.CacheItem.Key, out removedItem);
                    }
                }
            };

            MemoryCache.Default.Set(key, value, policy);
            AddRegionMapping(key, region);
        }

        /// <summary>
        /// Adds the region mapping.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="region">The region.</param>
        private void AddRegionMapping(string key, string region)
        {
            if (region.IsNotNull() && _regionMappings.ContainsKey(key).Is(false))
            {
                _regionMappings.TryAdd(key, region);
            }
        }

        private static DateTimeOffset EnsureExpiredTimeValid(DateTimeOffset? input)
        {
            var maxValue = DateTime.MaxValue.AddDays(-2); // avoid max value of timezone <0 to be greater than year 10000
            if (input.HasValue && input.Value < maxValue)
                return input.Value;

            return maxValue;
        }
    }
}
