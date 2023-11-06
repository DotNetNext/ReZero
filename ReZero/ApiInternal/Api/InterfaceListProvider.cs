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
            ZeroInterfaceList data = GetNewItem();
            data.HttpMethod = HttpRequestMethod.GET;
            data.Url = GetUrl(data, "GetZeroInterfaceList");
            zeroInterfaceList.Add(data);
        }
         
        public void GetInterfaceCategoryList()
        {
            ZeroInterfaceList data = GetNewItem();
            data.HttpMethod = HttpRequestMethod.GET;
            data.Url = GetUrl(data, "GetInterfaceCategoryList");
            zeroInterfaceList.Add(data);
        }
        private static ZeroInterfaceList GetNewItem()
        {
            return new ZeroInterfaceList()
            {
            };
        }

        private static string GetUrl(ZeroInterfaceList zeroInterface, string actionName)
        {
            return $"{NamingConventionsConst.ApiReZeroRoute}/{zeroInterface.InterfaceCategoryId}/{actionName}";
        } 
    }
}
