using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class InterfaceCategoryInitializerProvider
    {
        #region Root
        /// <summary>
        /// 根目录
        /// </summary>
        [ChineseTextAttribute("根目录")]
        [TextEN("Root")]
        public const long Id = 0;
        #endregion

        #region Swagger
        /// <summary>
        /// Swagger
        /// </summary>
        [ChineseTextAttribute("原生接口")]
        [TextEN("Swagger")]
        public const long Id1 = 1;
        #endregion


        #region Dynamic interface
        /// <summary>
        /// 自定义接口
        /// </summary>
        [ChineseTextAttribute("动态接口")]
        [TextEN("Dynamic interface")]
        public const long Id200 = 200;

        /// <summary>
        /// 测试分类1
        /// </summary>
        [ChineseTextAttribute("测试分类1")]
        [TextEN("Test 01")]
        public const long Id200100 = 200100;

        #endregion


        #region Internal interface
        /// <summary>
        /// 内置接口
        /// </summary>
        [ChineseTextAttribute("系统接口")]
        [TextEN("Internal interface")]
        public const long Id100 = 100;
         

        /// <summary>
        /// 页面布局
        /// </summary>
        [ChineseTextAttribute("页面布局")]
        [TextEN("Page layout")]
        public const long Id100002 = 100002;

        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口管理")]
        [TextEN("Interface list")]
        public const long Id100003 = 100003;


        /// <summary>
        /// 数据字典
        /// </summary>
        [ChineseTextAttribute("数据字典")]
        [TextEN("Dictionary")]
        public const long Id100004 = 100004;
        #endregion


        #region Project management
        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口管理")]
        [TextEN("Api management")]
        public const long Id300 = 300;

        /// <summary>
        /// 实体表管理
        /// </summary>
        [ChineseTextAttribute("实体表维护")]
        [TextEN("Entity and table management")]
        public const long Id300001 = 300001;

        /// <summary>
        /// 数据库管理
        /// </summary>
        [ChineseTextAttribute("数据库维护")]
        [TextEN("Database management")]
        public const long Id300003 = 300002;

        /// <summary>
        /// 接口分类管理
        /// </summary>
        [ChineseTextAttribute("分类维护")]
        [TextEN("InterfaceCategory")]
        public const long Id300002 = 300003;
         
        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口维护")]
        [TextEN("Api management")]
        public const long Id300006 = 300006;
        #endregion

    }
}