using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public enum ActionType
    {
        //Query
        QueryByPrimaryKey=10000,
        QueryCommon = 10001,
        QueryTree = 10002,

        //Insert
        InsertObject =20000,

        //Delete
        DeleteObject = 30000,
        BizDeleteObject = 30001,

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
