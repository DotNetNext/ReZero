using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class  CacheManager<T>: ICacheManager<T> where T:class,new()
    {
        private static List<T> cacheObject = null!;
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
            if (cacheObject != null)
            {
                return cacheObject;
            }

            lock (cacheLock)
            {
                if (cacheObject == null)
                {
                    var db = App.Db;
                    cacheObject = db.Queryable<T>().ToList();
                }
            }

            return cacheObject;
        }

        public void ClearCache()
        {
            lock (cacheLock)
            {
                cacheObject = null!;
            }
        }
    }
}