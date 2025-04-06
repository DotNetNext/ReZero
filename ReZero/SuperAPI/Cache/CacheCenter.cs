using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class CacheCenter
    {
        private static readonly object cacheLock = new object();
        public void ClearAllInternalCache() 
        {
            lock (cacheLock)
            {
                CacheManager<ZeroDatabaseInfo>.Instance.ClearCache();
                CacheManager<ZeroInterfaceList>.Instance.ClearCache();
                CacheManager<ZeroEntityInfo>.Instance.ClearCache();
                CacheManager<ZeroJwtTokenManagement>.Instance.ClearCache();
            }
        }
    }
}
