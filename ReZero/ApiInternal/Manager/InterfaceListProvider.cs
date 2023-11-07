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
            SetZeroInterfaceList();
            SetInterfaceCategory();
        }

        public void SetZeroInterfaceList()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.Query_Common;
                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100002();
                it.Url = GetUrl(it, "GetZeroInterfaceList");
            });
            zeroInterfaceList.Add(data);
        }
         
        public void SetInterfaceCategory()
        {
            ZeroInterfaceList data = GetNewItem(it => {

                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id200002();
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
            });
            zeroInterfaceList.Add(data);
        }
    }
}
