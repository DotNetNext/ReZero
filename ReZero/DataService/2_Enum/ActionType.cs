using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public enum ActionType
    {
        //Query
        QueryPrimaryKey=10000,
        QueryCommon = 10001,
        QueryTree = 10001,

        //Insert
        InsertObject =20000,

        //Delete
        DeleteObject = 30000,

        //Update
        UpdateObject = 40000,

        //DDL
        DllDatabaseList = 50000
    }
}
