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
        [EnglishTextAttribute("Database management")]
        public const long DbManId = 1;


        /// <summary>
        /// 获取数据库管理所有
        /// </summary> 
        [ChineseTextAttribute("获取数据库管理所有")]
        [EnglishTextAttribute("Database all list")]
        public const long GetDbAllId = 23;


        /// <summary>
        /// 删除数据库
        /// </summary> 
        [ChineseTextAttribute("删除数据库")]
        [EnglishTextAttribute("Daelete Database")]
        public const long DelDbManId = 11;

        /// <summary>
        /// 添加数据库
        /// </summary> 
        [ChineseTextAttribute("添加数据库")]
        [EnglishTextAttribute("Add Database")]
        public const long AddDbManId = 12;

        /// <summary>
        /// 修改库管理
        /// </summary> 
        [ChineseTextAttribute("添加数据库")]
        [EnglishTextAttribute("Edit Database")]
        public const long EditDbManId = 13;

        /// <summary>
        /// 获取数据库根据ID
        /// </summary> 
        [ChineseTextAttribute("获取数据库根据ID")]
        [EnglishTextAttribute("Get database by id")]
        public const long GetDbManIdById = 14;

        /// <summary>
        /// 测试数据库
        /// </summary> 
        [ChineseTextAttribute("测试数据库")]
        [EnglishTextAttribute("Test database")]
        public const long TestDatabaseId=16;

        /// <summary>
        /// 创建数据库
        /// </summary> 
        [ChineseTextAttribute("创建数据库")]
        [EnglishTextAttribute("Create database")]
        public const long CreateDatabaseId = 17;

        /// <summary>
        /// 内部接口
        /// </summary> 
        [ChineseTextAttribute("接口列表")]
        [EnglishTextAttribute("Internal interface list")]
        public const long IntIntListId = 2;
        /// <summary>
        /// 接口分类
        /// </summary> 
        [ChineseTextAttribute("动态分类列表")]
        [EnglishTextAttribute("Dynamic category list")]
        public const long IntCatePageListId = 3;

        /// <summary>
        /// 接口详情
        /// </summary>
        [ChineseTextAttribute("接口详情")]
        [EnglishTextAttribute("Interface Detail")]
        public const long IntDetId = 4;



        ///// <summary>
        ///// 动态接口[测试01]
        ///// </summary>
        //[ChineseTextAttribute("测试动态接口01")]
        //[EnglishTextAttribute("Test API 01")]
        //public const long TestId = 175179646053135000;


        /// <summary>
        /// 接口分类树
        /// </summary> 
        [ChineseTextAttribute("接口分类树")]
        [EnglishTextAttribute("Interface category tree")]
        public const long IntCateTreeId = 6;


        /// <summary>
        /// 添加动态接口分类
        /// </summary> 
        [ChineseTextAttribute("添加动态接口分类")]
        [EnglishTextAttribute("Add dynamic category")]
        public const long AddCateTreeId = 7;

        [ChineseTextAttribute("修改动态接口分类")]
        [EnglishTextAttribute("Update dynamic category")]
        public const long UpdateCateTreeId = 8;

        [ChineseTextAttribute("删除动态接口分类")]
        [EnglishTextAttribute("Delete dynamic category")]
        public const long DeleteCateTreeId = 9;


        [ChineseTextAttribute("根据主键查询接口分类")]
        [EnglishTextAttribute("Get category by id")]
        public const long GetCateTreeById = 10;

        [ChineseTextAttribute("下拉列表：获取数据库类型")]
        [EnglishTextAttribute("Get database type list")]

        public const long GetDbTypeListId = 15;

        [ChineseTextAttribute("下拉列表：获取c#类型")]
        [EnglishTextAttribute("Get c# type")]

        public const long GetNativeTypeId = 24;



        [ChineseTextAttribute("获取实体列表")]
        [EnglishTextAttribute("Get entity list")]

        public const long GetEntityInfoListId = 18;


        [ChineseTextAttribute("获取实体根据主键")]
        [EnglishTextAttribute("Get entity by id")]

        public const long GetEntityInfoById_Id = 19;

        [ChineseTextAttribute("删除实体")]
        [EnglishTextAttribute("Delete entity")]

        public const long DeleteEntityInfoById = 20;


        [ChineseTextAttribute("添加实体")]
        [EnglishTextAttribute("Add entity")]

        public const long AddEntityInfoId = 21;

        [ChineseTextAttribute("更新实体")]
        [EnglishTextAttribute("Update entity")]

        public const long UpdateEntityInfoId = 22;


        [ChineseTextAttribute("获取属性根据实体ID")]
        [EnglishTextAttribute("Get entity columns")]
        public const long GetEntityColumnsByEntityId_Id = 25;

        [ChineseTextAttribute("更新实体属性")]
        [EnglishTextAttribute("Update entity columns")]
        public const long UpdateEntityColumnInfosId= 26;

        [ChineseTextAttribute("表结构对比")]
        [EnglishTextAttribute("Compare database dtructure")]
        public const long CompareDatabaseStructureId = 27;


        [ChineseTextAttribute("创建表")]
        [EnglishTextAttribute("Create table")]
        public const long CreateTablesId = 28;

         
        [ChineseTextAttribute("获取导入的表")]
        [EnglishTextAttribute("Get import tables ")]
        public const long GetImportTablesId = 29;


        [ChineseTextAttribute("导入实体")]
        [EnglishTextAttribute("Import entities")]
        public const long ImportEntitiesId = 30;

         
        [ChineseTextAttribute("接口列表分页")]
        [EnglishTextAttribute("dynamic interface page list")]
        public const long DynamicIntPageListId = 31;


        [ChineseTextAttribute("删除动态接口")]
        [EnglishTextAttribute("Delete dynamic interface")]
        public const long DeleteDynamicIntId = 32;

         
        [ChineseTextAttribute("下拉列表：动态分类数据源")]
        [EnglishTextAttribute("Dynamic category datasource")]
        public const long IntCateListId = 33;


        [ChineseTextAttribute("下拉列表：动态分类中的分组集合")]
        [EnglishTextAttribute("Dynamic group name datasource")]
        public const long IntCateGroupNameListId = 34;


        [ChineseTextAttribute("下拉列表：获接口操作方式集合")]
        [EnglishTextAttribute("Get interface action list ")]
        public const long GetActionTypeId = 35;

        [ChineseTextAttribute("获取所有表")]
        [EnglishTextAttribute("Get all tables ")]
        public const long GetAllTablesId = 36;


        [ChineseTextAttribute("保存接口")]
        [EnglishTextAttribute("Save interface")]
        public const long SaveInterfaceListId = 37;


        [ChineseTextAttribute("下拉列表：获取条件类型")]
        [EnglishTextAttribute("Get where type list ")]
        public const long GetWhereTypeListId = 38;


        [ChineseTextAttribute("同步数据")]
        [EnglishTextAttribute("Synchronous Data")]
        public const long SynchronousDataId = 39;
        [ChineseTextAttribute("获取token")]
        [EnglishTextAttribute("Get 获取token")]
        public const long GetTokenId = 40;
        [ChineseTextAttribute("获取用户信息")]
        [EnglishTextAttribute("Get user info")]
        public const long GetUserInfoId = 41;

        [ChineseTextAttribute("执行SQL")]
        [EnglishTextAttribute("Execuet sql")]
        public const long ExecuetSqlId = 42;

        [ChineseTextAttribute("获取配置")]
        [EnglishTextAttribute("Get setting")]
        public const long GetSettingId = 43;
        [ChineseTextAttribute("更新配置")]
        [EnglishTextAttribute("Update setting")]
        public const long UpdateSettingId = 44;

        [ChineseTextAttribute("导出实体")]
        [EnglishTextAttribute("Export entities")]
        public const long ExportEntitiesId = 45;


        [ChineseTextAttribute("文件模版分页")]
        [EnglishTextAttribute("File template page")]
        public const long GetTemplatePageId = 46;

        [ChineseTextAttribute("文件模版根据id")]
        [EnglishTextAttribute("File template by id")]
        public const long GetTemplateById_Id = 47;

        [ChineseTextAttribute("添加文件模版")]
        [EnglishTextAttribute("Add template")]
        public const long GetAddTemplateId = 48;

        [ChineseTextAttribute("修改文件模版")]
        [EnglishTextAttribute("Update template")]
        public const long GetUpdateTemplateId = 49;

        [ChineseTextAttribute("删除文件模版")]
        [EnglishTextAttribute("Delete template")]
        public const long DeleteTemplateId = 50;

        [ChineseTextAttribute("获取模版分类")]
        [EnglishTextAttribute("Get template type")]
        public const long GetTemplateTypeId = 51;


        [ChineseTextAttribute("获取默认模版")]
        [EnglishTextAttribute("Get default template")]
        public const long GetDefalutTemplateId = 52;

        [ChineseTextAttribute("执行模版生成")]
        [EnglishTextAttribute("Execute template")]
        public const long ExecTemplateId = 53;

        [ChineseTextAttribute("获取默认模版json格式")]
        [EnglishTextAttribute("Get template tormat json")]
        public const long GetTemplateFormatJsonId = 54;

        [ChineseTextAttribute("获取模版根据分类")]
        [EnglishTextAttribute("Get template by type")]
        public const long GetTemplateByTypeId_Id = 55;

        [ChineseTextAttribute("生成实体")]
        [EnglishTextAttribute("Generate entity file")]
        public const long ExecTemplateByTableIdsId = 56;


        [ChineseTextAttribute("清除系统缓存")]
        [EnglishTextAttribute("Clear internal cache")]
        public const long ClearAllInternalCacheId = 57;


        [ChineseTextAttribute("根据SQL返回Excel")]
        [EnglishTextAttribute("Sql to excel")]
        public const long ExecuetSqlReturnExcelId = 58;

        [ChineseTextAttribute("保存接口配置")]
        [EnglishTextAttribute("Save config")]
        public const long SaveConfigId = 59;

        [ChineseTextAttribute("获取初始化配置")]
        [EnglishTextAttribute("Get init config")]
        public const long GetInitConfigId= 60;

        [ChineseTextAttribute("获取用户列表")]
        [EnglishTextAttribute("Get user list")]
        public const long GetUserInfoListId = 61;

        [ChineseTextAttribute("获取验证码")]
        [EnglishTextAttribute("Get verify code")]
        public const long VerifyCodeId = 62;

        [ChineseTextAttribute("保存用户")]
        [EnglishTextAttribute("Save User")]
        public const long SaveUserId = 63;
        [ChineseTextAttribute("根据主键获取用户")]
        [EnglishTextAttribute("Get user by id")]
        public const long GetUserById_Id = 64;
        [ChineseTextAttribute("删除用户")]
        [EnglishTextAttribute("Delete user by id")]
        public const long DeleteUserById_Id = 65;

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
