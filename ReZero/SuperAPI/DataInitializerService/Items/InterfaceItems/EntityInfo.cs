using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_EntityInfo()
        {
            GetEntityInoList();

            DeleteEntityInfo();

            AddEntityInfo();

            UpdateEntityInfo();

            GetEntityInfoById();

            ImportEntities();
        }

        private void GetEntityInfoById()
        {
            //根据主键获取实体
            ZeroInterfaceList data6 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetEntityInfoById_Id;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetEntityInfoById_Id);
                it.Url = GetUrl(it, "GetEntityInfoById");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.QueryByPrimaryKey,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroInterfaceCategory.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonText("主键", "Id") }
                         }
                };
            });
            zeroInterfaceList.Add(data6);
        }

        private void UpdateEntityInfo()
        {
            //修改实体
            ZeroInterfaceList data5 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = UpdateEntityInfoId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(UpdateEntityInfoId);
                it.Url = GetUrl(it, "UpdateEntityInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.UpdateObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityInfo.Id),ValueType = typeof(long).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityInfo.ClassName) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true,
                            IsUnique=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityInfo.DbTableName) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true,
                            IsUnique=true
                        } ,ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityInfo.DataBaseId), ParameterValidate= new ParameterValidate()
                        {
                            IsRequired=true
                        },ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() { Name=nameof(ZeroEntityInfo.Description),ValueType = typeof(string).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        }

        private void AddEntityInfo()
        {
            //添加实体
            ZeroInterfaceList data4 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = AddEntityInfoId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(AddEntityInfoId);
                it.Url = GetUrl(it, "AddEntityInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.InsertObject,
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() {
                                                   Name=nameof(ZeroEntityInfo.ClassName) ,
                                                   ParameterValidate=new ParameterValidate(){IsRequired=true,IsUnique=true},ValueType = typeof(string).Name },
                         new DataModelDefaultParameter() {
                                                   Name=nameof(ZeroEntityInfo.DbTableName) ,
                                                   ParameterValidate=new ParameterValidate(){IsRequired=true,IsUnique=true},ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() {
                                                  Name=nameof(ZeroEntityInfo.DataBaseId) ,
                                                  ParameterValidate=
                                                  new ParameterValidate()
                                                   {
                                                     IsRequired=true
                                                  },

                                                  ValueType = typeof(long).Name },
                       new DataModelDefaultParameter() {
                                                  Name=nameof(ZeroEntityInfo.Description) ,
                                                  ValueType = typeof(string).Name },
                                                  DataInitHelper.GetIsDynamicParameter(),
                        new DataModelDefaultParameter() {
                                                Name=nameof(ZeroEntityInfo.Creator),
                                                InsertParameter=new InsertParameter(){
                                                     IsUserName=true
                                                },
                                                 Value="" ,
                                                 ValueType = typeof(string).Name },
                        new DataModelDefaultParameter() {
                                                Name=nameof(ZeroEntityInfo.CreateTime),
                                                InsertParameter=new InsertParameter(){
                                                     IsDateTimeNow=true
                                                },
                                                 Value="" ,
                                                 ValueType = typeof(string).Name },

                    }
                };
            });
            zeroInterfaceList.Add(data4);
        }

        private void DeleteEntityInfo()
        {
            //实体删除
            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DeleteEntityInfoById;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(DeleteEntityInfoById);
                it.Url = GetUrl(it, "DeleteEntityInfo");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.BizDeleteObject,
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonText("主键", "Id") },
                              new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonText("是否删除", "IsDeleted") }
                         }
                };
            });
            zeroInterfaceList.Add(data3);
        }

        private void GetEntityInoList()
        {
            //获取实体列表
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetEntityInfoListId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetEntityInfoListId);
                it.Url = GetUrl(it, "GetEntityInoList");
                it.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid,
                    ResultColumnModels = new List<ResultColumnModel>()
                    {
                         //new ResultColumnModel()
                         //{
                         //      ResultColumnType=ResultColumnType.SubqueryName,
                         //      PropertyName= nameof(ZeroEntityInfo.DataBaseId), 
                         //},
                        new ResultColumnModel()
                         {
                               ResultColumnType=ResultColumnType.ConvertDefaultTimeString,
                               PropertyName= nameof(ZeroEntityInfo.CreateTime),
                         }
                    }
                };
                it.DataModel = new DataModel()
                {
                    CommonPage = new DataModelPageParameter
                    {
                        PageSize = 20,
                        PageNumber = 1
                    },
                    SelectParameters=new List<DataModelSelectParameters>() 
                    {
                          new DataModelSelectParameters()
                          {
                              TableIndex=0,
                              IsTableAll=true
                          },
                          new DataModelSelectParameters()
                          {
                              TableIndex=1,
                              Name=nameof(ZeroDatabaseInfo.Name),
                              AsName=PubConst.Orm_DataBaseNameDTO,
                              
                          }
                    },
                    JoinParameters=new List<DataModelJoinParameters>()
                    {  new DataModelJoinParameters()
                      {
                        JoinType=SqlSugar.JoinType.Left,
                        JoinTableId= EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                        OnList=new List<JoinParameter>()
                        {
                          new JoinParameter()
                          {
                                  LeftPropertyName=nameof(ZeroEntityInfo.DataBaseId),
                                  LeftIndex=0,
                                  FieldOperator=FieldOperatorType.Equal,
                                  RightPropertyName=nameof(ZeroDatabaseInfo.Id),
                                  RightIndex=1
                          },

                          }
                       },  
                    }, 
                    Columns = new List<DataColumnParameter>()
                    {

                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.Id) ,
                            Description=TextHandler.GetCommonText("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.ClassName) ,
                            Description=TextHandler.GetCommonText("实体名", "Class name")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.DbTableName) ,
                            Description=TextHandler.GetCommonText("表名", "Table name")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.Description) ,
                            Description=TextHandler.GetCommonText("备注", "Description")
                        },
                         new DataColumnParameter(){
                            PropertyName=PubConst.Orm_DataBaseNameDTO ,
                            Description=TextHandler.GetCommonText("数据库", "DataBase Name")
                        },
                        new DataColumnParameter(){
                            PropertyName=nameof(ZeroEntityInfo.IsInitialized) ,
                            Description=TextHandler.GetCommonText("系统数据", "System data")
                        },
                       new DataColumnParameter(){
                            PropertyName=nameof(ZeroEntityInfo.CreateTime) ,
                            Description=TextHandler.GetCommonText("创建时间", "Create time")
                        },
                       new DataColumnParameter(){
                            PropertyName=nameof(ZeroEntityInfo.ColumnCount) ,
                            Description=TextHandler.GetCommonText("列数", "Column count")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.QueryCommon,
                    OrderDynamicParemters=new List<DataModelDynamicOrderParemter>() { 
                      new DataModelDynamicOrderParemter(){  FieldName=nameof(ZeroInterfaceCategory.Id),OrderByType=SqlSugar.OrderByType.Desc }
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("IsDeleted", "IsDeleted") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.IsInitialized),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonText("是否系统数据", "IsInitialized") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.DataBaseId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name , Description = TextHandler.GetCommonText("数据库ID", "Database id") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.ClassName),   FieldOperator=FieldOperatorType.Like,  ValueType = typeof(string).Name,Value=null , Description = TextHandler.GetCommonText("名称", "class Name") },
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.DbTableName),MergeForName=nameof(ZeroEntityInfo.ClassName)  },
                             new DataModelDefaultParameter() { Name = nameof(ZeroEntityInfo.Description),MergeForName=nameof(ZeroEntityInfo.ClassName)  },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") },
                             new DataModelDefaultParameter() { Name="OrderByType" ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name, Description = TextHandler.GetCommonText("排序类型", "SortType") },
                             new DataModelDefaultParameter() { Name="OrderByName" ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(string).Name, Description = TextHandler.GetCommonText("排序字段", "SortName") }
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }

        private void ImportEntities()
        {
            //获取数据库所有
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = ImportEntitiesId;
                it.GroupName = nameof(ZeroEntityInfo);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(ImportEntitiesId);
                it.Url = GetUrl(it, "ImportEntities");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.MethodGeneratorAPI,
                    MyMethodInfo = new MyMethodInfo()
                    {
                        MethodArgsCount = 2,
                        ArgsTypes = new Type[] {typeof(long), typeof(List<string>) },
                        MethodClassFullName = typeof(MethodApi).FullName,
                        MethodName = nameof(MethodApi.ImportEntities)
                    },
                    DefaultParameters = new List<DataModelDefaultParameter>()
                    {
                        new DataModelDefaultParameter() { Name ="databasdeId",   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,  Description = TextHandler.GetCommonText("数据库Id", "Database id") },
                        new DataModelDefaultParameter() { Name ="tableNames",   FieldOperator=FieldOperatorType.Equal,  ValueType = PubConst.Orm_ApiParameterJsonArray,  Description = TextHandler.GetCommonText("List<string> 如：[表名1,表名2]", "List<string> [tableName1,tableName2]") },
                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
    }
}
