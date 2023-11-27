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
            AddZeroInterfaceList(datas);
            AddZeroInterfaceCategory(datas);
            AddZeroDataBaseInfo(datas);
            return datas;
        }

        private void AddZeroDataBaseInfo(List<ZeroEntityInfo> datas)
        { 
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroDatabaseInfo));
            data.Id = Id_ZeroDatabaseInfo;
            datas.Add(data);
        }

        private static void AddZeroInterfaceList(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroInterfaceList));
            data.Id = Id_ZeroInterfaceList;
            datas.Add(data);
        }
        private static void AddZeroInterfaceCategory(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroInterfaceCategory));
            data.Id = Id_ZeroInterfaceCategory;
            datas.Add(data);
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
