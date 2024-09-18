using System;
using System.Collections.Generic;
using System.Text;

namespace Omnia.MigrationG2.Foundation.Core.Caching
{
    public interface ICacheService
    {
        /// <summary>
        /// Gets item from the default MemoryCache
        /// </summary>
        /// <returns></returns>
        T GetFromMemoryCache<T>(string key, string region = "");

        /// <summary>
        /// Adds object to the default MemoryCache with expiry DateTimeOffset
        /// </summary>
        void AddOrUpdateMemoryCache(string key, object value, DateTimeOffset? expires, string region = "");

        /// <summary>
        /// Adds object to the default MemoryCache. The object will be cached until the appdomain ends
        /// </summary>
        void AddOrUpdateMemoryCache(string key, object value, string region = "");
    }
}
