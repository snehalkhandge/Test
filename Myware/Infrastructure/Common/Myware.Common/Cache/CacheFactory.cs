using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Common.Cache
{
    /// <summary>
    /// Cache Factory
    /// </summary>
    public class CacheFactory
    {
        #region Members

        static ICacheFactory _currentCacheFactory = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Set the  cache factory to use
        /// </summary>
        /// <param name="logFactory">Log factory to use</param>
        public static void SetCurrent(ICacheFactory cacheFactory)
        {
            _currentCacheFactory = cacheFactory;
        }

        /// <summary>
        /// Create a new Cache
        /// </summary>
        /// <returns>Created ICache</returns>
        public static ICache Create()
        {
            return (_currentCacheFactory != null) ? _currentCacheFactory.Create() : null;
        }

        #endregion
    }
}
