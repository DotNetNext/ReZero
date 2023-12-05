using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        public InterfaceListInitializerProvider(List<ZeroInterfaceList> zeroInterfaceList)
        {
            this.zeroInterfaceList = zeroInterfaceList;
        } 
        internal void Set()
        {
            GetZeroInterfaceList();
            GetInterfaceCategory();
            GetDatabaseList();
        }

        private void GetDatabaseList()
        {
            ZeroInterfaceList data = GetNewItem(it => { 
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DbManId;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id300003;
                it.Name = TextHandler.GetInterfaceListText(DbManId);
                it.Url = GetUrl(it, "GetDatabaseList"); 
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() { 
                            new WhereParameter(){ Name="Name" , FieldOperator=FieldOperatorType.Like, ValueType=typeof(string).Name,Description=TextHandler.GetCommonTexst("接口名称","Interface Name") },
                            DataInitHelper.GetIsInitializedParameter(),
                         }
                };
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetZeroInterfaceList()
        {
            Intenal();

            Dynamic();

        }

        private void Dynamic()
        {
            //动态测试接口
            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = TestId;
                it.GroupName =nameof(ZeroInterfaceList);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id200100;
                it.Name = TextHandler.GetInterfaceListText(TestId);
                it.Url ="/MyTest/API";
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.QueryByPrimaryKey,
                    WhereParameters = new List<WhereParameter>() {
                            new WhereParameter(){ Name="Id",IsRequired=true,FieldOperator=FieldOperatorType.Equal,  ValueType=typeof(long).Name, Description=TextHandler.GetCommonTexst("根据主键获取接口","Get interface detail") },
                         }
                };
                it.IsInitialized = false;
            });
            zeroInterfaceList.Add(data3);
        }

        private void Intenal()
        {
            //内部接口列表
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntIntListId;
                it.CustomResultModel = new ResultModel() { ResultType=ResultType.Group, GroupName=nameof(ZeroInterfaceList.GroupName) };
                it.GroupName = nameof(ZeroInterfaceList);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(IntIntListId);
                it.Url = GetUrl(it, "GetInternalInterfaceList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() {
                            new WhereParameter(){ Name="InterfaceCategoryId",FieldOperator=FieldOperatorType.In,  ValueType=typeof(long).Name, Description=TextHandler.GetCommonTexst("接口分类Id","Interface Category Id") },
                            new WhereParameter(){ Name="Name", FieldOperator=FieldOperatorType.Like, ValueType=typeof(string).Name, Description=TextHandler.GetCommonTexst("接口名称","Interface Name") },
                            new WhereParameter() { Name = "IsInitialized",FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name, Description = TextHandler.GetCommonTexst("是否内置数据", "Is initialized") },
                            new WhereParameter(){ Name="Url",MergeForName="Name",ValueIsReadOnly=true, FieldOperator=FieldOperatorType.Like, ValueType=typeof(string).Name, Description=TextHandler.GetCommonTexst("Url","Url") },
                    }
                };
            });
            zeroInterfaceList.Add(data);

            //内部接口列表详情
            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntDetId;
                it.GroupName = nameof(ZeroInterfaceList);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(IntDetId);
                it.Url = GetUrl(it, "GetInternalDetail");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.QueryByPrimaryKey,
                    WhereParameters = new List<WhereParameter>() {
                            new WhereParameter(){ Name="Id",IsRequired=true,FieldOperator=FieldOperatorType.Equal,  ValueType=typeof(long).Name, Description=TextHandler.GetCommonTexst("根据主键获取接口","Get interface detail") },
                         }
                };
            });
            zeroInterfaceList.Add(data2);
        }

        public void GetInterfaceCategory()
        { 
            //接口分类树
            ZeroInterfaceList data1 = GetNewItem(it => {
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
                    TreeParameter=new DataModelTreeParameter() 
                    {
                         ChildPropertyName=nameof(ZeroInterfaceCategory.SubInterfaceCategories),
                         RootValue=0,
                         CodePropertyName=nameof(ZeroInterfaceCategory.Id),
                         ParentCodePropertyName = nameof(ZeroInterfaceCategory.ParentId)
                    },
                    WhereParameters = new List<WhereParameter>()
                    {
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Id) ,Value=InterfaceCategoryInitializerProvider.Id,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonTexst("根目录ID", "Root id") },
                    }
                };
            });
            zeroInterfaceList.Add(data1);


            //接口分类列表
            ZeroInterfaceList data2 = GetNewItem(it => {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntCateListId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(IntCateListId);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() {
                             new WhereParameter() { Name = nameof(ZeroInterfaceCategory.ParentId),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=200,ValueIsReadOnly=true, Description = TextHandler.GetCommonTexst("上级Id", "ParentId") },
                               new WhereParameter() { Name = nameof(ZeroInterfaceCategory.Name),   FieldOperator=FieldOperatorType.Like,  ValueType = typeof(string).Name,Value=null , Description = TextHandler.GetCommonTexst("名称", "Name") }
                    } 
                };
            });
            zeroInterfaceList.Add(data2);


            //接口分类删除
            ZeroInterfaceList data3 = GetNewItem(it => {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = DeleteCateTreeId;
                it.GroupName = nameof(ZeroInterfaceCategory);
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(DeleteCateTreeId);
                it.Url = GetUrl(it, "DeleteInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                    ActionType = ActionType.BizDeleteObject,
                    WhereParameters = new List<WhereParameter>() {
                             new WhereParameter() { Name = nameof(ZeroInterfaceCategory.Id),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name,Value=0, Description = TextHandler.GetCommonTexst("主键", "Id") },
                              new WhereParameter() { Name = nameof(ZeroInterfaceCategory.IsDeleted),   FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name,Value="true", Description = TextHandler.GetCommonTexst("是否删除", "IsDeleted") }
                         }
                };
            });
            zeroInterfaceList.Add(data3);

            //添加动态接口分类
            ZeroInterfaceList data4 = GetNewItem(it => {
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
                    WhereParameters = new List<WhereParameter>()
                    {
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Name) ,ValueType = typeof(string).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.ParentId),Value=InterfaceCategoryInitializerProvider.Id200,ValueIsReadOnly=true,ValueType = typeof(long).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Description) ,ValueType = typeof(string).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Url),ValueIsReadOnly=true,Value= "/rezero/dynamic_interface.html?InterfaceCategoryId="+PubConst.TreeUrlFormatId,ValueType = typeof(string).Name },
                        DataInitHelper.GetIsDynamicParameter(),
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Creator),Value="" ,ValueType = typeof(string).Name },

                    }
                };
            });
            zeroInterfaceList.Add(data4);


            //修改动态接口分类
            ZeroInterfaceList data5 = GetNewItem(it => {
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
                    WhereParameters = new List<WhereParameter>()
                    {
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Id) ,ValueType = typeof(long).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Name) ,ValueType = typeof(string).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.ParentId),Value=InterfaceCategoryInitializerProvider.Id200,ValueIsReadOnly=true,ValueType = typeof(long).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Description) ,ValueType = typeof(string).Name },
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Url),ValueIsReadOnly=true,Value=null,ValueType = typeof(string).Name },
                        DataInitHelper.GetIsDynamicParameter(),
                        new WhereParameter() { Name=nameof(ZeroInterfaceCategory.Creator),Value="" ,ValueType = typeof(string).Name },
                    }
                };
            });
            zeroInterfaceList.Add(data5);
        }
    }
}
