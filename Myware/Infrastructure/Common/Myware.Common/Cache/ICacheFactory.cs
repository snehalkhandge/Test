using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myware.Common.Cache
{
    /// <summary>
    /// Base contract for Cache abstract factory
    /// </summary>
    public interface ICacheFactory
    {
        /// <summary>
        /// Create a new ICache
        /// </summary>
        /// <returns>The ICache created</returns>
        ICache Create();
    }
}
