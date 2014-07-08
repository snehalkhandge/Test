using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Common.Cache
{
    /// <summary>
    /// Interface of generic cache.
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// Gets caching object count.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Add object to cache container by specified expire sliding duration and callback.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="slidingDuration"></param>
        /// <param name="cachePriorityType"></param>
        void Add(object key, object value, TimeSpan slidingDuration, CachePriorityTypes cachePriorityType);

        /// <summary>
        /// Get caching object by key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(object key);

        /// <summary>
        /// Remove caching object by key.
        /// </summary>
        /// <param name="key"></param>
        void Remove(object key);
    }
}
