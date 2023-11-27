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
                it.ActionType = ActionType.QueryCommon;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = Id1;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id300003;
                it.Name = TextHandler.GetInterfaceListText(Id1);
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
            ZeroInterfaceList data = GetNewItem(it =>
            {
                it.ActionType = ActionType.QueryCommon;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = Id2;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100002;
                it.Name = TextHandler.GetInterfaceListText(Id2);
                it.Url = GetUrl(it, "GetInternalInterfaceList"); 
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() {
                            new WhereParameter(){ Name="InterfaceCategoryId",FieldOperator=FieldOperatorType.In,  ValueType=typeof(long).Name, Description=TextHandler.GetCommonTexst("接口分类Id","Interface Category Id") },
                            new WhereParameter(){ Name="Name", FieldOperator=FieldOperatorType.Like, ValueType=typeof(string).Name, Description=TextHandler.GetCommonTexst("接口名称","Interface Name") },
                            new WhereParameter() { Name = "IsInitialized",FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(bool).Name, Description = TextHandler.GetCommonTexst("是否内置数据", "Is initialized") ,
                            } 
                    }
                };
            });
            zeroInterfaceList.Add(data);

            ZeroInterfaceList data2 = GetNewItem(it =>
            {
                it.ActionType = ActionType.QueryByPrimaryKey;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = Id4;
                it.InterfaceCategoryId = 0;
                it.Name = TextHandler.GetInterfaceListText(Id4);
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


            ZeroInterfaceList data3 = GetNewItem(it =>
            {
                it.ActionType = ActionType.QueryByPrimaryKey;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = TestId;
                it.InterfaceCategoryId = 0;
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

        public void GetInterfaceCategory()
        {
            
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.QueryTree;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = Id3;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100001;
                it.Name = TextHandler.GetInterfaceListText(Id3);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                         TableId= EntityInfoInitializerProvider.Id_ZeroInterfaceCategory,
                         ActionType=ActionType.QueryCommon,
                         WhereParameters=new List<WhereParameter>() {
                           
                         }
                };
            });
            zeroInterfaceList.Add(data);
        }
    }
}
