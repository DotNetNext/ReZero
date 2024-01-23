using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_Other()
        {
            GetImportTables();
            GetActionType();
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
                    ActionType = ActionType.MyMethod,
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
                    ActionType = ActionType.MyMethod,
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
                    ActionType = ActionType.MyMethod,
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
    }
}
