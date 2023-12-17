using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {
        private void AddInit_EntityList()
        {
            GetEntityInoList();

            DeleteEntityInfo();

            AddEntityInfo();

            UpdateEntityInfo();

            GetEntityInfoById();
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
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") }
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
                    DefaultParameters = new List<DefaultParameter>()
                    {
                        new DefaultParameter() { Name=nameof(ZeroEntityInfo.Id),ValueType = typeof(long).Name },
                        new DefaultParameter() { Name=nameof(ZeroEntityInfo.ClassName) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroEntityInfo.DbTableName) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroEntityInfo.DataBaseId), ParameterValidate= new ParameterValidate()
                        {
                            IsRequired=true
                        },ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroEntityInfo.Description),ValueType = typeof(string).Name },
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
                    DefaultParameters = new List<DefaultParameter>()
                    {
                        new DefaultParameter() {
                                                   Name=nameof(ZeroEntityInfo.ClassName) ,
                                                   ParameterValidate=new ParameterValidate(){IsRequired=true},ValueType = typeof(string).Name },
                         new DefaultParameter() {
                                                   Name=nameof(ZeroEntityInfo.DbTableName) ,
                                                   ParameterValidate=new ParameterValidate(){IsRequired=true},ValueType = typeof(string).Name },
                        new DefaultParameter() {
                                                  Name=nameof(ZeroEntityInfo.DataBaseId) ,
                                                  ParameterValidate=
                                                  new ParameterValidate()
                                                   {
                                                     IsRequired=true
                                                  },

                                                  ValueType = typeof(long).Name },
                       new DefaultParameter() {
                                                  Name=nameof(ZeroEntityInfo.Description) ,
                                                  ValueType = typeof(string).Name },
                                                  DataInitHelper.GetIsDynamicParameter(),
                        new DefaultParameter() {
                                                Name=nameof(ZeroEntityInfo.Creator),
                                                InsertParameter=new InsertParameter(){
                                                     IsUserName=true
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
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroEntityInfo.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") },
                              new DefaultParameter() { Name = nameof(ZeroEntityInfo.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonTexst("是否删除", "IsDeleted") }
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
                         new ResultColumnModel()
                         {
                               ResultColumnType=ResultColumnType.SubqueryName,
                               PropertyName= nameof(ZeroEntityInfo.DataBaseId),

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
                    Columns = new List<DataColumnParameter>()
                    {

                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.Id) ,
                            Description=TextHandler.GetCommonTexst("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.ClassName) ,
                            Description=TextHandler.GetCommonTexst("类名", "Class name")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.DbTableName) ,
                            Description=TextHandler.GetCommonTexst("表名", "Table name")
                        },
                         new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.DataBaseId) ,
                            Description=TextHandler.GetCommonTexst("数据库", "DataBase id")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroEntityInfo.Description) ,
                            Description=TextHandler.GetCommonTexst("备注", "Description")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroEntityInfo,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonTexst("IsDeleted", "IsDeleted") },
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.Name),   FieldOperator=FieldOperatorType.Like,  ValueType = typeof(string).Name,Value=null , Description = TextHandler.GetCommonTexst("名称", "class Name") },
                             new DefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("第几页", "Page number") },
                             new DefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("每页几条", "Pageize") }
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }
    }
}
