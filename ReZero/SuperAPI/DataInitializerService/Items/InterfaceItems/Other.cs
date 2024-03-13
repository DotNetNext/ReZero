using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_Other()
        {
            GetImportTables();
            GetActionType();
            GetAllTables();
            SaveInterfaceList();
            GetWhereTypeList();
        }

        private void SaveInterfaceList()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = SaveInterfaceListId;
                it.GroupName = nameof(ZeroInterfaceList);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(SaveInterfaceListId);
                it.Url = GetUrl(it, "SaveInterfaceList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 1,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.SaveInterfaceList)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { IsSingleParameter=true, Name ="model",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(JObject).Name,  Description = TextHandler.GetCommonText("动态json", "json parameter") },
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        } 
        private void GetImportTables()
        {
            //获取导入的表
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetImportTablesId;
                it.GroupName = nameof(DbTableInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetImportTablesId);
                it.Url = GetUrl(it, "GetImportTables");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo=new MyMethodInfo() {  
                         MethodArgsCount=2,
                          MethodClassFullName=typeof(MethodApi).FullName,
                           MethodName= nameof(MethodApi.GetImportTables)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("库ID", "DatabaseId") },
                         new DataModelDefaultParameter() { Name ="tableName",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("表名", "Table name") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetAllTables()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetAllTablesId;
                it.GroupName = nameof(DbTableInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetAllTablesId);
                it.Url = GetUrl(it, "GetAllTables");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetTables)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("库ID", "DatabaseId") },
                         new DataModelDefaultParameter() { Name ="tableName",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("表名", "Table name") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetActionType()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetActionTypeId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetActionTypeId);
                it.Url = GetUrl(it, "GetActionType");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 0,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetActionType)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                       
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetWhereTypeList()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetWhereTypeListId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100004;
                it.Name = TextHandler.GetInterfaceListText(GetWhereTypeListId);
                it.Url = GetUrl(it, "GetWhereTypeList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 0,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetWhereTypeList)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {

                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
    }
}
