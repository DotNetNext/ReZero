using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class InterfaceListProvider
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        public InterfaceListProvider(List<ZeroInterfaceList> zeroInterfaceList)
        {
            this.zeroInterfaceList = zeroInterfaceList;
        } 
        internal void Set()
        {
        }
        public void GetZeroInterfaceList()
        {
            ZeroInterfaceList data = GetNewItem(it => {
                it.ActionType = ActionType.Query_Common;
                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id100002();
                it.Url = GetUrl(it, "GetZeroInterfaceList");
            });
            zeroInterfaceList.Add(data);
        }
         
        public void GetInterfaceCategoryList()
        {
            ZeroInterfaceList data = GetNewItem(it => {

                it.HttpMethod = HttpRequestMethod.GET;
                it.InterfaceCategoryId = InterfaceCategoryProvider.Id200002();
                it.Url = GetUrl(it, "GetInterfaceCategoryList");
            });
            zeroInterfaceList.Add(data);
        }
        private static ZeroInterfaceList GetNewItem(Action<ZeroInterfaceList> action)
        {
            var result = new ZeroInterfaceList()
            {
                IsInitialized = true, 
            };
            action(result);
            return result;
        }

        private static string GetUrl(ZeroInterfaceList zeroInterface, string actionName)
        {
            return $"{NamingConventionsConst.ApiReZeroRoute}/{zeroInterface.InterfaceCategoryId}/{actionName}";
        } 
    }
}
