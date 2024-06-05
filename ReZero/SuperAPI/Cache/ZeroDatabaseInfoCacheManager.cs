using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class ZeroDatabaseInfoCacheManager
    {
        private static List<ZeroDatabaseInfo> cacheZeroDatabaseInfo = null!;
        private static readonly object cacheLock = new object();

        public static ZeroDatabaseInfoCacheManager Instance
        {
            get
            {
                return new ZeroDatabaseInfoCacheManager();
            }
        }
        public List<ZeroDatabaseInfo> GetZeroDatabaseInfo()
        {
            if (cacheZeroDatabaseInfo != null)
            {
                return cacheZeroDatabaseInfo;
            }

            lock (cacheLock)
            {
                if (cacheZeroDatabaseInfo == null)
                {
                    var db = App.Db;
                    cacheZeroDatabaseInfo = db.Queryable<ZeroDatabaseInfo>().ToList();
                }
            }

            return cacheZeroDatabaseInfo;
        }

        public void ClearZeroDatabaseInfoCache()
        {
            lock (cacheLock)
            {
                cacheZeroDatabaseInfo = null!;
            }
        }
    }
}