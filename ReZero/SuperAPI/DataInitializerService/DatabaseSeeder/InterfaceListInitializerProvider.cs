using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero.SuperAPI
{
    internal partial class InterfaceListInitializerProvider
    {
        /// <summary>
        /// 数据库管理
        /// </summary> 
        [TextCN("数据库管理")]
        [TextEN("Database management")]
        public const long DbManId = 1;
        /// <summary>
        /// 内部接口
        /// </summary> 
        [TextCN("接口列表")]
        [TextEN("Internal interface list")]
        public const long IntIntListId = 2;
        /// <summary>
        /// 接口分类
        /// </summary> 
        [TextCN("动态分类列表")]
        [TextEN("Dynamic category list")]
        public const long IntCateListId = 3;

        /// <summary>
        /// 接口详情
        /// </summary>
        [TextCN("接口详情")]
        [TextEN("Interface Detail")]
        public const long IntDetId = 4;



        /// <summary>
        /// 动态接口[测试01]
        /// </summary>
        [TextCN("测试动态接口01")]
        [TextEN("Test API 01")]
        public const long TestId = 5;


        /// <summary>
        /// 接口分类树
        /// </summary> 
        [TextCN("接口分类树")]
        [TextEN("Interface category tree")]
        public const long IntCateTreeId = 6;


        /// <summary>
        /// 添加动态接口分类
        /// </summary> 
        [TextCN("添加动态接口分类")]
        [TextEN("Add dynamic category")]
        public const long AddCateTreeId = 7;

        [TextCN("修改动态接口分类")]
        [TextEN("Update dynamic category")]
        public const long UpdateCateTreeId = 8;

        [TextCN("删除动态接口分类")]
        [TextEN("Delete dynamic category")]
        public const long DeleteCateTreeId = 9;


        [TextCN("根据主键查询接口分类")]
        [TextEN("Get category by id")]
        public const long GetCateTreeById = 10;

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
