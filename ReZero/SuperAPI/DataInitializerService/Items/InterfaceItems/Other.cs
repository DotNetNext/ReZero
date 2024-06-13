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
            GetToKen();
            GetUserInfo();
            ExecuetSql();
            GetSetting();
            UpdateSetting();
            ExportEntities();
            GetDefalutTemplate();
            GetTemplateFormatJson();
            ExecTemplate();
            ExecTemplateByTableIds();
            ClearAllInternalCache();
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
        private void ExecuetSql()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ExecuetSqlId;
                it.GroupName = nameof(DbTableInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ExecuetSqlId);
                it.Url = GetUrl(it, "ExecuetSql");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExecuetSql)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="DatabaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("库ID", "DatabaseId") },
                        new DataModelDefaultParameter() { Name ="Sql",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("Sql", "Sql") }
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
        private void GetToKen()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.All.ToString();
                it.Id = GetTokenId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id300;
                it.Name = TextHandler.GetInterfaceListText(GetTokenId);
                it.Url = "/api/rezero/token";
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        ArgsTypes=new Type[] {typeof(string),typeof(string) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetToken)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                         new DataModelDefaultParameter()
                         {
                              Name="UserName",ParameterValidate=new ParameterValidate(){ IsRequired=true }, ValueType=typeof(string).Name, Description=TextHandler.GetCommonText("用户名","User name")
                         },
                          new DataModelDefaultParameter()
                         {
                              Name="Password",ParameterValidate=new ParameterValidate(){ IsRequired=true }, ValueType=typeof(string).Name, Description=TextHandler.GetCommonText("密码","Password")
                         }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        } 
        private void GetUserInfo()
        {
             
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.All.ToString();
                it.Id = GetUserInfoId;
                it.GroupName = nameof(MethodApi);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id300;
                it.Name = TextHandler.GetInterfaceListText(GetUserInfoId);
                it.Url = "/api/rezero/getuserinfo";
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 0,
                        ArgsTypes = new Type[] {  },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetUserInfo)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                          
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetSetting()
        { 
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetSettingId;
                it.GroupName = nameof(ZeroSysSetting);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetSettingId);
                it.Url = GetUrl(it, "GetSetting");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetSetting)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="typeId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("分类ID", "Type id") },
                        new DataModelDefaultParameter() { Name ="childTypeId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("子分类Id", "Child type id") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void UpdateSetting()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = UpdateSettingId;
                it.GroupName = nameof(ZeroSysSetting);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(UpdateSettingId);
                it.Url = GetUrl(it, "UpdateSetting");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 3,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.UpdateSetting)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="typeId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("分类ID", "Type id") },
                        new DataModelDefaultParameter() { Name ="childTypeId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("子分类Id", "Child type id") },
                        new DataModelDefaultParameter() { Name ="value",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("值", "Value") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void ExportEntities()
        { 
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ExportEntitiesId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ExportEntitiesId);
                it.Url = GetUrl(it, "ExportEntities");
                it.CustomResultModel = new ResultModel()
                {
                     ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                     GroupName=TextHandler.GetCommonText( "数据库文档{0}.xlsx", "Tables{0}.xlsx"),
                     ResultType=ResultType.File
                };
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        ArgsTypes=new Type[] {typeof(long), typeof(long[]) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExportEntities)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("数据库Id", "Database id") },
                        new DataModelDefaultParameter() { Name ="tableIds",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(JsonArray).Name,  Description = TextHandler.GetCommonText("表Id集合", "Table id array") }  
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetDefalutTemplate()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetDefalutTemplateId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetDefalutTemplateId);
                it.Url = GetUrl(it, "GetDefalutTemplate"); 
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 1, 
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetDefalutTemplate)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="type",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("模版分类ID", "template type id") }, 
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void GetTemplateFormatJson()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetTemplateFormatJsonId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetTemplateFormatJsonId);
                it.Url = GetUrl(it, "GetTemplateFormatJson");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 1,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.GetTemplateFormatJson)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="type",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("模版分类ID", "template type id") },
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void ExecTemplate()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ExecTemplateId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ExecTemplateId);
                it.Url = GetUrl(it, "ExecTemplate");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 3,
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExecTemplate)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="type",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(int).Name,  Description = TextHandler.GetCommonText("模版分类ID", "template type id") },
                        new DataModelDefaultParameter() { Name ="data",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("数据", "data") },
                        new DataModelDefaultParameter() { Name ="template",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("模版字符串", "template") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        } 
        private void ExecTemplateByTableIds()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ExecTemplateByTableIdsId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ExecTemplateByTableIdsId);
                it.Url = GetUrl(it, "ExecTemplateByTableIds"); 
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 4,
                        ArgsTypes = new Type[] { typeof(long), typeof(long[]),typeof(long),typeof(string) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExecTemplateByTableIds)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("数据库Id", "Database id") },
                        new DataModelDefaultParameter() { Name ="tableIds",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(JsonArray).Name,  Description = TextHandler.GetCommonText("表Id集合", "Table id array") },
                        new DataModelDefaultParameter() { Name ="templateId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("模版ID", "template id") },
                        new DataModelDefaultParameter() { Name ="url",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("Url", "Url") }
                    } 
                };
            });
            zeroInterfaceList.Add(data1);
        }
        private void ClearAllInternalCache()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ClearAllInternalCacheId;
                it.GroupName = nameof(CacheCenter);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ClearAllInternalCacheId);
                it.Url = GetUrl(it, "ClearAllInternalCache");
                it.DataModel = new DataModel()
                {
                    TableId =0,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 0, 
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ClearAllInternalCache)
                    } 
                };
            });
            zeroInterfaceList.Add(data1);
        }

        private void ExecuetSqlReturnExcel()
        {
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ExecuetSqlReturnExcelId;
                it.GroupName = nameof(DbTableInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ExecuetSqlReturnExcelId);
                it.Url = GetUrl(it, "ExecuetSqlReturnExcel");
                it.CustomResultModel = new ResultModel()
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    GroupName = TextHandler.GetCommonText("数据库文档{0}.xlsx", "Tables{0}.xlsx"),
                    ResultType = ResultType.File
                };
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        ArgsTypes = new Type[] { typeof(long), typeof(string) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExecuetSqlReturnExcel)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="DatabaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("库ID", "DatabaseId") },
                        new DataModelDefaultParameter() { Name ="Sql",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("Sql", "Sql") }
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
    }
}
