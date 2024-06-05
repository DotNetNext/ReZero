using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class ZeroInterfaceListCacheManager
    {
        private static List<ZeroInterfaceList> cacheZeroInterfaceList = null!;
        private static readonly object cacheLock = new object();

        public static ZeroInterfaceListCacheManager Instance
        {
            get
            {
                return new ZeroInterfaceListCacheManager();
            }
        }
        public List<ZeroInterfaceList> GetZeroInterfaceList()
        {
            if (cacheZeroInterfaceList != null)
            {
                return cacheZeroInterfaceList;
            }

            lock (cacheLock)
            {
                if (cacheZeroInterfaceList == null)
                {
                    var db = App.Db;
                    cacheZeroInterfaceList = db.Queryable<ZeroInterfaceList>().ToList();
                }
            }

            return cacheZeroInterfaceList;
        }

        public void ClearZeroInterfaceListCache()
        {
            lock (cacheLock)
            {
                cacheZeroInterfaceList = null!;
            }
        }
    }
}