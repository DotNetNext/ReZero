using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero
{
    internal partial class InterfaceListInitializerProvider
    {
        /// <summary>
        /// 数据库管理
        /// </summary> 
        [TextCN("数据库管理")]
        [TextEN("Database management")]
        public const long Id1 = 1;
        /// <summary>
        /// 内部接口
        /// </summary> 
        [TextCN("内部接口列表")]
        [TextEN("Internal interface list")]
        public const long Id2 = 2;
        /// <summary>
        /// 接口分类
        /// </summary> 
        [TextCN("接口分类")]
        [TextEN("Interface classification")]
        public const long Id3 = 3;

        private static ZeroInterfaceList GetNewItem(Action<ZeroInterfaceList> action)
        {
            var result = new ZeroInterfaceList()
            {
                IsInitialized = true,
                DataModel = new DataModel()
            };
            action(result);
            return result;
        }

        private static string GetUrl(ZeroInterfaceList zeroInterface, string actionName)
        {
            return $"/{NamingConventionsConst.ApiReZeroRoute}/{zeroInterface.InterfaceCategoryId}/{actionName}";
        }
    }
}
