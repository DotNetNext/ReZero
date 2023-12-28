using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_EntityColumnInfo()
        {
            GetEntityColuminsByEntityId();
            SaveEntityColumnInfos(); 
        }
        private void GetEntityColuminsByEntityId()
        { 
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetEntityColumnsByEntityId_Id;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetEntityColumnsByEntityId_Id);
                it.Url = GetUrl(it, "GetEntityColuminsByEntityId");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityColumnInfo.TableId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonText("实体Id", "Entity id") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }

        private void SaveEntityColumnInfos()
        {
            //修改实体
            ZeroInterfaceList data5 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = UpdateEntityColumnInfosId;
                it.GroupName = nameof(ZeroEntityColumnInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(UpdateEntityColumnInfosId);
                it.Url = GetUrl(it, "SaveEntityColumnInfos");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroColumnInfo,
                    ActionType = ActionType.MyMethod,
                    MyMethodInfo=new MyMethodInfo()
                    {
                        MethodArgsCount = 1,
                        MethodClassFullName = typeof(MethodApi_Easy).FullName,
                        MethodName = nameof(MethodApi_Easy.AddOrUpdateEntityColumninfos)

                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {  
                        new DataModelDefaultParameter() { Name="Columns", Description="List<ZeroEntityColumnInfo>序列化的Json格式",ValueType = typeof(string).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        } 
    }
}