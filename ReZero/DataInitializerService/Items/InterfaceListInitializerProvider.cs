using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
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
                it.Parameters = new List<ZeroInterfaceParameter>()
                { 
                    new ZeroInterfaceParameter(){ Name="Name", ValueType=typeof(string).Name,Description=TextHandler.GetCommonTexst("接口名称","Interface Name") },
                };
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroDatabaseInfo,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() { 
                           new WhereParameter(){ ProperyName=nameof(ZeroDatabaseInfo.Name) , FieldOperator=FieldOperatorType.Like },
                         }
                };
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetZeroInterfaceList()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.QueryCommon;
                it.HttpMethod = HttpRequestMethod.GET.ToString();
                it.Id = Id2;
                it.InterfaceCategoryId = InterfaceCategoryInitializerProvider.Id100002;
                it.Name = TextHandler.GetInterfaceListText(Id2);
                it.Url = GetUrl(it, "GetInternalInterfaceList");
                it.Parameters = new List<ZeroInterfaceParameter>()
                {
                    new ZeroInterfaceParameter(){ Name="InterfaceCategoryId",  ValueType=typeof(long).Name, Description=TextHandler.GetCommonTexst("接口分类Id","Interface Category Id") },
                    new ZeroInterfaceParameter(){ Name="Name", ValueType=typeof(string).Name,Description=TextHandler.GetCommonTexst("接口名称","Interface Name") },
                };
                it.DataModel = new DataModel()
                {
                    TableId = EntityInfoInitializerProvider.Id_ZeroInterfaceList,
                    ActionType = ActionType.QueryCommon,
                    WhereParameters = new List<WhereParameter>() {
                           new WhereParameter(){ ProperyName=nameof(ZeroInterfaceList.InterfaceCategoryId), FieldOperator=FieldOperatorType.Equal },
                           new WhereParameter(){ ProperyName=nameof(ZeroInterfaceList.Name) , FieldOperator=FieldOperatorType.Like }, 
                         }
                };
            });
            zeroInterfaceList.Add(data);
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
                           new WhereParameter(){ ProperyName=nameof(ZeroInterfaceCategory.Id), FieldOperator=FieldOperatorType.Equal },
                           new WhereParameter(){ ProperyName=nameof(ZeroInterfaceCategory.Name) , FieldOperator=FieldOperatorType.Like },
                         }
                };
            });
            zeroInterfaceList.Add(data);
        }
    }
}
