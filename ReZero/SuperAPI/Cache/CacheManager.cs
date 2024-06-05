using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class  CacheManager<T>: ICacheManager<T> where T:class,new()
    {
        private static List<T> cacheZeroDatabaseInfo = null!;
        private static readonly object cacheLock = new object();

        public static CacheManager<T> Instance
        {
            get
            {
                return new CacheManager<T>();
            }
        }
        public List<T> GetList()
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
                    cacheZeroDatabaseInfo = db.Queryable<T>().ToList();
                }
            }

            return cacheZeroDatabaseInfo;
        }

        public void ClearCache()
        {
            lock (cacheLock)
            {
                cacheZeroDatabaseInfo = null!;
            }
        }
    }
}