using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Myware.Common.Cache
{
    /// <summary>
    /// Cache implementation bases on http runtime cache.
    /// </summary>
    public class HttpRuntimeCache : ICache
    {
        /// <summary>
        /// Gets caching object count.
        /// </summary>
        public int Count { get { return HttpRuntime.Cache.Count; } }

        /// <summary>
        /// Add object to cache container by specified expire sliding duration and priority type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingDuration"></param>
        /// <param name="cachePriorityType"></param>
        public void Add(object key, object value, TimeSpan slidingDuration, CachePriorityTypes cachePriorityType)
        {
            StaticUtility.NotNull(key, "key");

            object cachedObject = HttpRuntime.Cache.Get(key.ToString());
            if (cachedObject != null)
                HttpRuntime.Cache.Remove(key.ToString());

            HttpRuntime.Cache.Add(key.ToString(), value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingDuration, (CacheItemPriority)Enum.Parse(typeof(CacheItemPriority), cachePriorityType.ToString()), null);
        }

        /// <summary>
        /// Get caching object by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(object key)
        {
            StaticUtility.NotNull(key, "key");
            return HttpRuntime.Cache.Get(key.ToString());
        }

        /// <summary>
        /// Remove caching object by key.
        /// </summary>
        /// <param name="key"></param>
        public void Remove(object key)
        {
            StaticUtility.NotNull(key, "key");
            HttpRuntime.Cache.Remove(key.ToString());
        }
    }
}
