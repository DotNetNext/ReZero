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
        [ChineseTextAttribute("数据库管理")]
        [TextEN("Database management")]
        public const long DbManId = 1;


        /// <summary>
        /// 获取数据库管理所有
        /// </summary> 
        [ChineseTextAttribute("获取数据库管理所有")]
        [TextEN("Database all list")]
        public const long GetDbAllId = 23;


        /// <summary>
        /// 删除数据库
        /// </summary> 
        [ChineseTextAttribute("删除数据库")]
        [TextEN("Daelete Database")]
        public const long DelDbManId = 11;

        /// <summary>
        /// 添加数据库
        /// </summary> 
        [ChineseTextAttribute("添加数据库")]
        [TextEN("Add Database")]
        public const long AddDbManId = 12;

        /// <summary>
        /// 修改库管理
        /// </summary> 
        [ChineseTextAttribute("添加数据库")]
        [TextEN("Edit Database")]
        public const long EditDbManId = 13;

        /// <summary>
        /// 获取数据库根据ID
        /// </summary> 
        [ChineseTextAttribute("获取数据库根据ID")]
        [TextEN("Get database by id")]
        public const long GetDbManIdById = 14;

        /// <summary>
        /// 测试数据库
        /// </summary> 
        [ChineseTextAttribute("测试数据库")]
        [TextEN("Test database")]
        public const long TestDatabaseId=16;

        /// <summary>
        /// 创建数据库
        /// </summary> 
        [ChineseTextAttribute("创建数据库")]
        [TextEN("Create database")]
        public const long CreateDatabaseId = 17;

        /// <summary>
        /// 内部接口
        /// </summary> 
        [ChineseTextAttribute("接口列表")]
        [TextEN("Internal interface list")]
        public const long IntIntListId = 2;
        /// <summary>
        /// 接口分类
        /// </summary> 
        [ChineseTextAttribute("动态分类列表")]
        [TextEN("Dynamic category list")]
        public const long IntCateListId = 3;

        /// <summary>
        /// 接口详情
        /// </summary>
        [ChineseTextAttribute("接口详情")]
        [TextEN("Interface Detail")]
        public const long IntDetId = 4;



        /// <summary>
        /// 动态接口[测试01]
        /// </summary>
        [ChineseTextAttribute("测试动态接口01")]
        [TextEN("Test API 01")]
        public const long TestId = 5;


        /// <summary>
        /// 接口分类树
        /// </summary> 
        [ChineseTextAttribute("接口分类树")]
        [TextEN("Interface category tree")]
        public const long IntCateTreeId = 6;


        /// <summary>
        /// 添加动态接口分类
        /// </summary> 
        [ChineseTextAttribute("添加动态接口分类")]
        [TextEN("Add dynamic category")]
        public const long AddCateTreeId = 7;

        [ChineseTextAttribute("修改动态接口分类")]
        [TextEN("Update dynamic category")]
        public const long UpdateCateTreeId = 8;

        [ChineseTextAttribute("删除动态接口分类")]
        [TextEN("Delete dynamic category")]
        public const long DeleteCateTreeId = 9;


        [ChineseTextAttribute("根据主键查询接口分类")]
        [TextEN("Get category by id")]
        public const long GetCateTreeById = 10;

        [ChineseTextAttribute("下拉列表：获取数据库类型")]
        [TextEN("Get database type list")]

        public const long GetDbTypeListId = 15;

        [ChineseTextAttribute("下拉列表：获取c#类型")]
        [TextEN("Get c# type")]

        public const long GetNativeTypeId = 24;



        [ChineseTextAttribute("获取实体列表")]
        [TextEN("Get entity list")]

        public const long GetEntityInfoListId = 18;


        [ChineseTextAttribute("获取实体根据主键")]
        [TextEN("Get entity by id")]

        public const long GetEntityInfoById_Id = 19;

        [ChineseTextAttribute("删除实体")]
        [TextEN("Delete entity")]

        public const long DeleteEntityInfoById = 20;


        [ChineseTextAttribute("添加实体")]
        [TextEN("Add entity")]

        public const long AddEntityInfoId = 21;

        [ChineseTextAttribute("更新实体")]
        [TextEN("Update entity")]

        public const long UpdateEntityInfoId = 22;


        [ChineseTextAttribute("获取属性根据实体ID")]
        [TextEN("Get entity columns")]
        public const long GetEntityColumnsByEntityId_Id = 25;

        [ChineseTextAttribute("更新实体属性")]
        [TextEN("Update entity columns")]
        public const long UpdateEntityColumnInfosId= 26;

        [ChineseTextAttribute("表结构对比")]
        [TextEN("Compare database dtructure")]
        public const long CompareDatabaseStructureId = 27;


        [ChineseTextAttribute("创建表")]
        [TextEN("Create table")]
        public const long CreateTablesId = 28;

         
        [ChineseTextAttribute("获取库里面所有表")]
        [TextEN("Get all tables ")]
        public const long GetTableAllId = 29;


        [ChineseTextAttribute("导入实体")]
        [TextEN("Import entities")]
        public const long ImportEntitiesId = 30;

         
        [ChineseTextAttribute("接口列表分页")]
        [TextEN("dynamic interface page list")]
        public const long DynamicIntPageListId = 31;


        [ChineseTextAttribute("删除动态接口")]
        [TextEN("Delete dynamic interface")]
        public const long DeleteDynamicIntId = 32;


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
