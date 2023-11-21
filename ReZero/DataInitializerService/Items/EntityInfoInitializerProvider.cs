using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public partial class EntityInfoInitializerProvider
    {

        internal List<ZeroEntityInfo> GetDatas()
        {
            List<ZeroEntityInfo> datas = new List<ZeroEntityInfo>(); 
            var entityMappingService = new EntityMappingService();
            var dbTable = entityMappingService.ConvertEntityToDbTableInfo(typeof(ZeroEntityInfo));
            dbTable.Id = Id_ZeroEntityInfo;
            return datas;
        }
        private static ZeroEntityInfo GetNewItem(Action<ZeroEntityInfo> action)
        {
            var result = new ZeroEntityInfo()
            {
                IsInitialized = true,
            };
            action(result);
            return result;
        }
    }
}
