using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
            ExecuetSqlReturnExcel();
            GetUserInfoPageList();
            GetZeroJwtTokenManagementPage();
        }
        private void GetZeroJwtTokenManagementPage()
        {
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetZeroJwtTokenManagementPageId;
                it.GroupName = nameof(ZeroJwtTokenManagement);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetZeroJwtTokenManagementPageId);
                it.Url = GetUrl(it, "GetZeroJwtTokenManagementPage");
                it.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid,
                    ResultColumnModels = new List<ResultColumnModel>()
                    {
                        new ResultColumnModel()
                        {
                            ResultColumnType = ResultColumnType.ConvertDefaultTimeString,
                            PropertyName = nameof(ZeroJwtTokenManagement.CreateTime),
                        },
                        new ResultColumnModel()
                        {
                            ResultColumnType = ResultColumnType.ConvertDefaultTimeString,
                            PropertyName = nameof(ZeroJwtTokenManagement.Expiration),
                        }
                    },

                };

                it.DataModel = new DataModel()
                {
                    Columns = new List<DataColumnParameter>()
                    {
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.UserName) ,
                            Description=TextHandler.GetCommonText("用户名", "User name")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.Description) ,
                            Description=TextHandler.GetCommonText("描述", "Description")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.Expiration) ,
                            Description=TextHandler.GetCommonText("使用期限", "Expiration")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.Token) ,
                            Description=TextHandler.GetCommonText("JWT Token", "JWT Token")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.Creator) ,
                            Description=TextHandler.GetCommonText("创建人", "Creator")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroJwtTokenManagement.CreateTime) ,
                            Description=TextHandler.GetCommonText("创建时间", "Create time")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroJwtTokenManagement,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                        new DataModelDefaultParameter() { Name = nameof(ZeroJwtTokenManagement.UserName),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name, Description = TextHandler.GetCommonText("用户名", "User name") },
                        new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageNumberPropName ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                        new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageSizePropName ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Page size") },
                        new DataModelDefaultParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("IsDeleted", "IsDeleted") },
                    },
                    CommonPage = new DataModelPageParameter()
                    {
                        PageNumber = 1,
                        PageSize = 20
                    },
                    OrderDynamicParemters = new List<DataModelDynamicOrderParemter>() {
                      new DataModelDynamicOrderParemter(){  FieldName=nameof(ZeroJwtTokenManagement.Id),OrderByType=SqlSugar.OrderByType.Desc }
                    },
                };
            });
            zeroInterfaceList.Add(data);
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
                        MethodArgsCount = 5,
                        ArgsTypes = new Type[] { typeof(long), typeof(long[]),typeof(long),typeof(string),typeof(string) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ExecTemplateByTableIds)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databaseId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("数据库Id", "Database id") },
                        new DataModelDefaultParameter() { Name ="tableIds",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(JsonArray).Name,  Description = TextHandler.GetCommonText("表Id集合", "Table id array") },
                        new DataModelDefaultParameter() { Name ="templateId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("模版ID", "template id") },
                        new DataModelDefaultParameter() { Name ="url",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("Url", "Url") },
                        new DataModelDefaultParameter() { Name ="viewName",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name,  Description = TextHandler.GetCommonText("View", "View") }
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

        private void GetUserInfoPageList()
        {
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetUserInfoListId;
                it.GroupName = nameof(ZeroUserInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetUserInfoListId);
                it.Url = GetUrl(it, "GetUserInfoPageList");
                it.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid,
                    ResultColumnModels = new List<ResultColumnModel>()
                    { 
                        new ResultColumnModel()
                         {
                               ResultColumnType=ResultColumnType.ConvertDefaultTimeString,
                               PropertyName= nameof(ZeroEntityInfo.CreateTime),
                         }
                    }
                }; 
                it.DataModel = new DataModel()
                {
                    Columns = new List<DataColumnParameter>()
                    {
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.UserName) ,
                            Description=TextHandler.GetCommonText("用户名", "User name")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.Password) ,
                            Description=TextHandler.GetCommonText("密码", "Password")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.BusinessAccount) ,
                            Description=TextHandler.GetCommonText("业务账号", "Business account")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.IsMasterAdmin) ,
                            Description=TextHandler.GetCommonText("管理员", "admin")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.EasyDescription) ,
                            Description=TextHandler.GetCommonText("备注", "Description")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.Creator) ,
                            Description=TextHandler.GetCommonText("创建人", "Creator")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroUserInfo.CreateTime) ,
                            Description=TextHandler.GetCommonText("创建时间", "Create time") 
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroUserInfo,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                        new DataModelDefaultParameter() { Name = nameof(ZeroUserInfo.UserName),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name, Description = TextHandler.GetCommonText("用户名", "User name") },
                        new DataModelDefaultParameter() { Name = nameof(ZeroUserInfo.IsMasterAdmin),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,  Description = TextHandler.GetCommonText("是否是管理员", "Is master admin") },
                        new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageNumberPropName ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                        new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageSizePropName ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") },
                        new DataModelDefaultParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("IsDeleted", "IsDeleted") },
                    },
                    CommonPage = new DataModelPageParameter()
                    {
                        PageNumber = 1,
                        PageSize = 20
                    },
                };
            });
            zeroInterfaceList.Add(data2);
        }
    }
}
