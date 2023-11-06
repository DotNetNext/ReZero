using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class InternalApiManager
    {
        List<ZeroInterfaceList> zeroInterfaceList = new List<ZeroInterfaceList>() { };
        public void Initialize(ReZeroOptions options)
        {
            var db = App.PreStartupDb;
            if (!db!.Queryable<ZeroInterfaceList>().Any())
                db!.Insertable(zeroInterfaceList).ExecuteCommand();
        }

        public void GetZeroInterfaceList()
        {
            ZeroInterfaceList data = NewZeroInterfaceList();
            data.HttpMethod = HttpRequestMethod.GET;
            data.Url = GetUrl(data, "GetZeroInterfaceList");
            zeroInterfaceList.Add(data);
        }

        public void GetInterfaceCategoryList()
        {
            ZeroInterfaceList data = NewZeroInterfaceList();
            data.HttpMethod = HttpRequestMethod.GET;
            data.Url = GetUrl(data, "GetInterfaceCategoryList");
            zeroInterfaceList.Add(data); 
        }


        public static void AddDataBase()
        {
             
        }
        private static string GetUrl(ZeroInterfaceList zeroInterface, string actionName)
        {
            return $"{NamingConventionsConst.ApiReZeroRoute}/{zeroInterface.InterfaceCategoryId}/{actionName}";
        }


        private static ZeroInterfaceList NewZeroInterfaceList()
        {
            return new ZeroInterfaceList()
            {
            };
        }
    }
}
