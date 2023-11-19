using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public partial class InterfaceCategoryInitializerProvider
    {
        #region Root
        /// <summary>
        /// 根目录
        /// </summary>
        [TextCN("根目录")]
        [TextEN("Root")]
        public const long Id = 0;
        #endregion

        #region Swagger
        /// <summary>
        /// Swagger
        /// </summary>
        [TextCN("原生接口")]
        [TextEN("Swagger")]
        public const long Id1 = 1;
        #endregion


        #region Dynamic interface
        /// <summary>
        /// 自定义接口
        /// </summary>
        [TextCN("动态接口")]
        [TextEN("Dynamic interface")]
        public const long Id200 = 200;

        /// <summary>
        /// 全部接口
        /// </summary>
        [TextCN("全部接口")]
        [TextEN("All interfaces")]
        public const long Id200001 = 200001;
        #endregion


        #region Internal interface
        /// <summary>
        /// 内置接口
        /// </summary>
        [TextCN("内置接口")]
        [TextEN("Internal interface")]
        public const long Id100 = 100;


        /// <summary>
        /// 全部接口
        /// </summary>
        [TextCN("全部接口")]
        [TextEN("All interfaces")]
        public const long Id100001 = 100001;

        /// <summary>
        /// 页面布局
        /// </summary>
        [TextCN("页面布局")]
        [TextEN("Page layout")]
        public const long Id100002 = 100002;

        /// <summary>
        /// 接口管理
        /// </summary>
        [TextCN("接口管理")]
        [TextEN("Interface list")]
        public const long Id100003 = 100003;
        #endregion


        #region Project management
        /// <summary>
        /// 项目管理
        /// </summary>
        [TextCN("项目管理")]
        [TextEN("Project management")]
        public const long Id300 = 300;

        /// <summary>
        /// 实体表管理
        /// </summary>
        [TextCN("实体表管理")]
        [TextEN("Entity and table management")]
        public const long Id300004 = 300001;

        /// <summary>
        /// 数据库管理
        /// </summary>
        [TextCN("数据库管理")]
        [TextEN("Database management")]
        public const long Id300003 = 300002;

        /// <summary>
        /// 项目分类
        /// </summary>
        [TextCN("项目分类")]
        [TextEN("Project classification")]
        public const long Id300002 = 300003;
        #endregion

    }
}