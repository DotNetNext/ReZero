using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class EntityInfoInitializerProvider
    {

        internal List<ZeroEntityInfo> GetDatas()
        {
            List<ZeroEntityInfo> datas = new List<ZeroEntityInfo>();
            AddZeroInterfaceList(datas);
            AddZeroInterfaceCategory(datas);
            AddZeroDataBaseInfo(datas);
            AddZeroEntityInfo(datas);
            AddZeroColumn(datas);
            AddZeroTemplate(datas);
            AddZeroTemplateType(datas);
            AddZeroUserInfo(datas);
            AddZeroJwtTokenManagement(datas);
            return datas;
        }
        private void AddZeroUserInfo(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroUserInfo));
            data.Id = Id_ZeroUserInfo;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }
        private void AddZeroColumn(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroEntityColumnInfo));
            data.Id = Id_ZeroColumnInfo;
            data.DataBaseId = DataBaseInitializerProvider.Id; 
            CommonSetting(data);
            datas.Add(data);
        }


        private void AddZeroEntityInfo(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroEntityInfo));
            data.Id = Id_ZeroEntityInfo;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }

        private void AddZeroDataBaseInfo(List<ZeroEntityInfo> datas)
        { 
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroDatabaseInfo));
            data.Id = Id_ZeroDatabaseInfo;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }

        private  void AddZeroInterfaceList(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroInterfaceList));
            data.Id = Id_ZeroInterfaceList;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }
        private  void AddZeroInterfaceCategory(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroInterfaceCategory));
            data.Id = Id_ZeroInterfaceCategory;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }

        private void AddZeroTemplate(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroTemplate));
            data.Id = Id_ZeroTemplate;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        }
        private void AddZeroTemplateType(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroTemplateType));
            data.Id = Id_ZeroTemplateType;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        } 
        private void AddZeroJwtTokenManagement(List<ZeroEntityInfo> datas)
        {
            var entityMappingService = new EntityMappingService();
            var data = entityMappingService.ConvertDbToEntityInfo(typeof(ZeroJwtTokenManagement));
            data.Id = Id_ZeroJwtTokenManagement;
            data.DataBaseId = DataBaseInitializerProvider.Id;
            CommonSetting(data);
            datas.Add(data);
        } 
        private void CommonSetting(ZeroEntityInfo data)
        {
            data.IsInitialized = true;
            data.IsDeleted = false;
        }
    }
}
