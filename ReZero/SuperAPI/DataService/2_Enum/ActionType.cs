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
        [EnglishText("biz Delete by entity")]
        BizDeleteObject = 30001, 
        #endregion

        //Update
        UpdateObject = 40000,

        //DDL
        DllDatabaseList = 50000,
        DllCreateDb = 50001, 
        DllCreateTables = 50002,
        DllGetTables=50003,

        //My method
        MyMethod = 99999,
    }
}
