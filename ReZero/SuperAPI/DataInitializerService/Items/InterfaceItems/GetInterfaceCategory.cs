﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal partial class InterfaceListInitializerProvider
    {

        public void AddInit_InterfaceCategory()
        {
            GetInterfaceCategoryList();

            GetDynamicInterfaceCategoryList();

            DeleteDynamicInterfaceCategory();

            AddDynamicInterfaceCategory();

            UpdateDynamicInterfaceCategory();

            GetDynamicInterfaceCategoryById();
        }

        private void GetDynamicInterfaceCategoryById()
        {
            //动态接口分类根据主键获取详情
            ZeroInterfaceList data6 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = GetCateTreeById;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(GetCateTreeById);
                it.Url = GetUrl(it, "GetDynamicInterfaceCategoryById");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.QueryByPrimaryKey,
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") }
                         }
                };
            });
            zeroInterfaceList.Add(data6);
        }

        private void UpdateDynamicInterfaceCategory()
        {
            //修改动态接口分类
            ZeroInterfaceList data5 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = UpdateCateTreeId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(UpdateCateTreeId);
                it.Url = GetUrl(it, "UpdateDynamicInterfaceCategory");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.UpdateObject,
                    DefaultParameters = new List<DefaultParameter>()
                    {
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Id),ValueType = typeof(long).Name },
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Name) ,ParameterValidate=
                        new ParameterValidate()
                        {
                            IsRequired=true
                        } ,ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Description),ValueType = typeof(string).Name }
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        }

        private void AddDynamicInterfaceCategory()
        {
            //添加动态接口分类
            ZeroInterfaceList data4 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.POST.ToString();
                it.Id = AddCateTreeId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(AddCateTreeId);
                it.Url = GetUrl(it, "AddDynamicInterfaceCategory");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.InsertObject,
                    DefaultParameters = new List<DefaultParameter>()
                    {
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Name) ,ParameterValidate=
                        new ParameterValidate()
                        {
                             IsRequired=true
                        },ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.ParentId),Value=InterfaceCategoryInitializerProvider.Id200,ValueIsReadOnly=true,ValueType = typeof(long).Name },
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Description) ,ValueType = typeof(string).Name },
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Url),ValueIsReadOnly=true,Value= "/rezero/dynamic_interface.html?InterfaceCategoryId="+PubConst.TreeUrlFormatId,ValueType = typeof(string).Name },
                        DataInitHelper.GetIsDynamicParameter(),
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Creator),
                        InsertParameter=new InsertParameter(){
                             IsUserName=true
                        },Value="" ,ValueType = typeof(string).Name },

                    }
                };
            });
            zeroInterfaceList.Add(data4);
        }

        private void DeleteDynamicInterfaceCategory()
        {
            //动态接口分类删除
            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DeleteCateTreeId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(DeleteCateTreeId);
                it.Url = GetUrl(it, "DeleteDynamicInterfaceCategory");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.BizDeleteObject,
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") },
                              new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonTexst("是否删除", "IsDeleted") }
                         }
                };
            });
            zeroInterfaceList.Add(data3);
        }

        private void GetDynamicInterfaceCategoryList()
        {
            //获取动态接口分类
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntCateListId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(IntCateListId);
                it.Url = GetUrl(it, "GetDynamicInterfaceCategoryList");
                it.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid
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
                            PropertyName= nameof(ZeroInterfaceCategory.Id) ,
                            Description=TextHandler.GetCommonTexst("ID", "Primary key")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroInterfaceCategory.Name) ,
                            Description=TextHandler.GetCommonTexst("名称", "Name")
                        },
                       new DataColumnParameter(){
                            PropertyName= nameof(ZeroInterfaceCategory.Description) ,
                            Description=TextHandler.GetCommonTexst("备注", "Description")
                        },
                        new DataColumnParameter(){
                            PropertyName= nameof(ZeroInterfaceCategory.Url) ,
                            Description=TextHandler.GetCommonTexst("跳转地址", "Url")
                        }
                    },
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.QueryCommon,
                    DefaultParameters = new List<DefaultParameter>() {
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.ParentId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=200,ValueIsReadOnly=true, Description = TextHandler.GetCommonTexst("上级Id", "ParentId") },
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="false",ValueIsReadOnly=true, Description = TextHandler.GetCommonTexst("IsDeleted", "IsDeleted") },
                             new DefaultParameter() { Name = nameof(ZeroInterfaceCategory.Name),   FieldOperator=FieldOperatorType.Like,  ValueType = typeof(string).Name,Value=null , Description = TextHandler.GetCommonTexst("名称", "Name") },
                             new DefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("第几页", "Page number") },
                             new DefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("每页几条", "Pageize") }
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }

        private void GetInterfaceCategoryList()
        {
            //接口分类树
            ZeroInterfaceList data1 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntCateTreeId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100002;
                it.Name = TextHandler.GetInterfaceListText(IntCateTreeId);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.QueryTree,
                    TreeParameter = new DataModelTreeParameter()
                    {
                        ChildPropertyName = nameof(ZeroInterfaceCategory.SubInterfaceCategories),
                        RootValue = 0,
                        CodePropertyName = nameof(ZeroInterfaceCategory.Id),
                        ParentCodePropertyName = nameof(ZeroInterfaceCategory.ParentId),
                    },
                    DefaultParameters = new List<DefaultParameter>()
                    {
                        new DefaultParameter() { Name=nameof(ZeroInterfaceCategory.Id) ,Value=InterfaceCategoryInitializerProvider.Id,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("根目录ID", "Root id") },

                    }
                };
            });
            zeroInterfaceList.Add(data1);
        }
    }
}