using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class ZeroEntityInfoCacheManager
    {
        private static List<ZeroEntityInfo> cacheZeroEntityInfo = null!;
        private static readonly object cacheLock = new object();

        public static ZeroEntityInfoCacheManager Instance
        {
            get
            {
                return new ZeroEntityInfoCacheManager();
            }
        }
        public List<ZeroEntityInfo> GetList()
        {
            if (cacheZeroEntityInfo != null)
            {
                return cacheZeroEntityInfo;
            }

            lock (cacheLock)
            {
                if (cacheZeroEntityInfo == null)
                {
                    var db = App.Db;
                    cacheZeroEntityInfo = db.Queryable<ZeroEntityInfo>().ToList();
                }
            }

            return cacheZeroEntityInfo;
        }

        public void ClearCache()
        {
            lock (cacheLock)
            {
                cacheZeroEntityInfo = null!;
            }
        }
    }
}