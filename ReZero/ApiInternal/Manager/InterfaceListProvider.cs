using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal partial class InterfaceListProvider
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        public InterfaceListProvider(List<ZeroInterfaceList> zeroInterfaceList)
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
                it.HttpMethod = HttpRequestMethod.GET;
                it.Id = Id1;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id300003;
                it.Name = TextHandler.GetInterfaceListText(Id1);
                it.Url = GetUrl(it, "GetDatabaseList");
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetZeroInterfaceList()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.QueryCommon;
                it.HttpMethod = HttpRequestMethod.GET;
                it.Id = Id2;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100002;
                it.Name = TextHandler.GetInterfaceListText(Id2);
                it.Url = GetUrl(it, "GetInternalInterfaceList");
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetInterfaceCategory()
        {
            
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.QueryCommon;
                it.HttpMethod = HttpRequestMethod.GET;
                it.Id = Id3;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100001;
                it.Name = TextHandler.GetInterfaceListText(Id3);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
                it.DataModel = new DataModel()
                {
                        
                };
            });
            zeroInterfaceList.Add(data);
        }
    }
}
