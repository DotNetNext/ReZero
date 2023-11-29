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
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id200100;
                it.Name = TextHandler.GetInterfaceListText(TestId);
                it.Url = GetUrl(it, "GetInternalDetail");
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
            //接口分类列表
            ZeroInterfaceList data = GetNewItem(it => { 
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntCateListId;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100003;
                it.Name = TextHandler.GetInterfaceListText(IntCateListId);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                         TableId= EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                         ActionType=ActionType.QueryCommon,
                         WhereParameters=new List<WhereParameter>() {
                             new WhereParameter() { Name = "IsInitialized" ,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name, Description = TextHandler.GetCommonTexst("是否内置数据", "Is initialized") },
                             new WhereParameter() { Name = "Id1",FieldName="Id",  ValueIsReadOnly=true,FieldOperator=FieldOperatorType.NoEqual,  ValueType = typeof(long).Name,Value=InterfaceCategoryInitializerProvider.Id, Description = TextHandler.GetCommonTexst("主键", "Id") },
                             new WhereParameter() { Name = "Id2" ,FieldName="Id", ValueIsReadOnly=true,FieldOperator=FieldOperatorType.NoEqual,  ValueType = typeof(long).Name,Value=InterfaceCategoryInitializerProvider.Id100, Description = TextHandler.GetCommonTexst("主键", "Id") },
                             new WhereParameter() { Name = "Id3",FieldName="Id",  ValueIsReadOnly=true,FieldOperator=FieldOperatorType.NoEqual,  ValueType = typeof(long).Name,Value=InterfaceCategoryInitializerProvider.Id1, Description = TextHandler.GetCommonTexst("主键", "Id") },
                             new WhereParameter() { Name = "Id4",FieldName="Id",  ValueIsReadOnly=true,FieldOperator=FieldOperatorType.NoEqual,  ValueType = typeof(long).Name,Value=InterfaceCategoryInitializerProvider.Id200, Description = TextHandler.GetCommonTexst("主键", "Id") }
                         }
                };
            });
            zeroInterfaceList.Add(data);

            //接口分类树
            ZeroInterfaceList data2 = GetNewItem(it => {
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = IntCateTreeId;
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
                       
                    }
                };
            });
            zeroInterfaceList.Add(data2);
        }
    }
}
