using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero
{
    internal partial class InterfaceListProvider
    {

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
