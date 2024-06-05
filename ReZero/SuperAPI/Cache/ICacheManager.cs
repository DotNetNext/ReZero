using System.Collections.Generic;

namespace ReZero.SuperAPI
{
    public interface ICacheManager<T>
    {
        void ClearCache();
        List<T> GetList();
    }
}