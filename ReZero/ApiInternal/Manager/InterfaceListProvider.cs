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
                it.ActionType = ActionType.Query_Common;
                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id200003;
                it.Name = TextHandler.GetInterfaceCategoryText(InterfaceCategoryProvider.Id200003);
                it.Url = GetUrl(it, "GetDatabaseList");
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetZeroInterfaceList()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.Query_Common;
                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100002;
                it.Name = TextHandler.GetInterfaceCategoryText(InterfaceCategoryProvider.Id100002);
                it.Url = GetUrl(it, "GetInterfaceList");
            });
            zeroInterfaceList.Add(data);
        } 
        public void GetInterfaceCategory()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.Query_Common;
                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100001;
                it.Name = TextHandler.GetInterfaceCategoryText(InterfaceCategoryProvider.Id100001);
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
            });
            zeroInterfaceList.Add(data);
        }
    }
}
