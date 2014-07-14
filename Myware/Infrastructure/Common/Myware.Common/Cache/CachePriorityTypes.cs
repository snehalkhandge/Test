using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Common.Cache
{
    /// <summary>
    /// Caching priority types
    /// </summary>
    public enum CachePriorityTypes
    {
        /// <summary>
        /// Cache items with this priority level are the most likely to be deleted from
        ///     the cache as the server frees system memory.
        /// </summary>
        Low = 1,

        /// <summary>
        /// Cache items with this priority level are more likely to be deleted from the
        ///     cache as the server frees system memory than items assigned a System.Web.Caching.CacheItemPriority.Normal
        ///     priority.
        /// </summary>
        BelowNormal = 2,

        /// <summary>
        /// Cache items with this priority level are likely to be deleted from the cache
        ///     as the server frees system memory only after those items with System.Web.Caching.CacheItemPriority.Low
        ///     or System.Web.Caching.CacheItemPriority.BelowNormal priority. This is the
        ///     default.
        /// </summary>
        Normal = 3,

        /// <summary>
        /// The default value for a cached item's priority is System.Web.Caching.CacheItemPriority.Normal.
        /// </summary>
        Default = 3,

        /// <summary>
        /// Cache items with this priority level are less likely to be deleted as the
        ///     server frees system memory than those assigned a System.Web.Caching.CacheItemPriority.Normal
        ///     priority.
        /// </summary>
        AboveNormal = 4,

        /// <summary>
        /// Cache items with this priority level are the least likely to be deleted from
        ///     the cache as the server frees system memory.
        /// </summary>
        High = 5,

        /// <summary>
        ///  The cache items with this priority level will not be automatically deleted
        ///     from the cache as the server frees system memory. However, items with this
        ///     priority level are removed along with other items according to the item's
        ///     absolute or sliding expiration time.
        /// </summary>
        NotRemovable = 6,
    }
}
