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
        [EnglishTextAttribute("Root")]
        public const long Id = 0;
        #endregion

        #region Swagger
        /// <summary>
        /// Swagger
        /// </summary>
        [ChineseTextAttribute("原生接口")]
        [EnglishTextAttribute("Swagger")]
        public const long Id1 = 1;
        #endregion


        #region Dynamic interface
        /// <summary>
        /// 自定义接口
        /// </summary>
        [ChineseTextAttribute("动态接口")]
        [EnglishTextAttribute("Dynamic interface")]
        public const long Id200 = 200;

        /// <summary>
        /// 测试分类1
        /// </summary>
        [ChineseTextAttribute("测试分类1")]
        [EnglishTextAttribute("Test 01")]
        public const long Id200100 = 200100;

        #endregion


        #region Internal interface
        /// <summary>
        /// 内置接口
        /// </summary>
        [ChineseTextAttribute("系统接口")]
        [EnglishTextAttribute("Internal interface")]
        public const long Id100 = 100;
         

        /// <summary>
        /// 页面布局
        /// </summary>
        [ChineseTextAttribute("页面布局")]
        [EnglishTextAttribute("Page layout")]
        public const long Id100002 = 100002;

        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口管理")]
        [EnglishTextAttribute("Interface list")]
        public const long Id100003 = 100003;


        /// <summary>
        /// 数据字典
        /// </summary>
        [ChineseTextAttribute("数据字典")]
        [EnglishTextAttribute("Dictionary")]
        public const long Id100004 = 100004;
        #endregion


        #region Project management
        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口管理")]
        [EnglishTextAttribute("Api management")]
        public const long Id300 = 300;

        /// <summary>
        /// 实体表管理
        /// </summary>
        [ChineseTextAttribute("实体表维护")]
        [EnglishTextAttribute("Entity and table management")]
        public const long Id300001 = 300001;

        /// <summary>
        /// 数据库管理
        /// </summary>
        [ChineseTextAttribute("数据库维护")]
        [EnglishTextAttribute("Database management")]
        public const long Id300003 = 300002;

        /// <summary>
        /// 接口分类管理
        /// </summary>
        [ChineseTextAttribute("分类维护")]
        [EnglishTextAttribute("InterfaceCategory")]
        public const long Id300002 = 300003;
         
        /// <summary>
        /// 接口管理
        /// </summary>
        [ChineseTextAttribute("接口维护")]
        [EnglishTextAttribute("Api management")]
        public const long Id300006 = 300006;


        /// <summary>
        /// 接口授权
        /// </summary>
        [ChineseTextAttribute("接口授权")]
        [EnglishTextAttribute("Api authorization")]
        public const long Id300007 = 300007;

        /// <summary>
        /// 文件模版
        /// </summary>
        [ChineseTextAttribute("文件模版")]
        [EnglishTextAttribute("File template")]
        public const long Id300008 = 300008;

        /// <summary>
        /// 系统缓存
        /// </summary>
        [ChineseTextAttribute("系统缓存")]
        [EnglishTextAttribute("System cache")]
        public const long Id300009 = 300009;
        #endregion

        #region System setting
        [ChineseTextAttribute("系统&配置")]
        [EnglishTextAttribute("System setting")]
        public const long SystemSettingId = 400; 
        #endregion

        #region Data document
        [ChineseTextAttribute("数据文档")]
        [EnglishTextAttribute("Data document")]
        public const long DataDocumentRootId = 500;
        [ChineseTextAttribute("数据文档")]
        [EnglishTextAttribute("Data document")]
        public const long DataDocumentManagerId = 500001;
        #endregion

    }
}