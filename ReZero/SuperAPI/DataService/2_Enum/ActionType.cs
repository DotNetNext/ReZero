using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public enum ActionType
    {
         
        #region Query
        [ChineseText("根据主键查询")]
        [EnglishText("Query by primary key")]
        QueryByPrimaryKey = 10000,

        [ChineseText("通用查询")]
        [EnglishText("Common query")]
        QueryCommon = 10001,

        [ChineseText("树型查询")]
        [EnglishText("Common tree")]
        QueryTree = 10002,
        #endregion


        #region Insert
        [ChineseText("插入根据实体")]
        [EnglishText("Insert by entity")]
        InsertObject = 20000,
        #endregion


        #region Delete
        [ChineseText("删除根据实体")]
        [EnglishText("Delete by entity")]
        DeleteObject = 30000,  

        [ChineseText("逻辑删除根据实体")]
        [EnglishText("logic delete by entity")]
        BizDeleteObject = 30001,
        #endregion


        #region  Update
        [ChineseText("更新根据实体")]
        [EnglishText("Update by entity")]
        UpdateObject = 40000,
        #endregion


        #region DDL
        [ChineseText("获取数据库")]
        [EnglishText("Get database list")]
        DllDatabaseList = 50000,
        [ChineseText("创建数据库")]
        [EnglishText("Create database")]
        DllCreateDb = 50001,
        [ChineseText("创建表")]
        [EnglishText("Create tables")]
        DllCreateTables = 50002,
        [ChineseText("获取表")]
        [EnglishText("Get tables")]
        DllGetTables = 50003,
        #endregion


        #region 自定义方法
        [ChineseText("自定义方法")]
        [EnglishText("My method")]
        MyMethod = 99999, 
        #endregion
    }
}
