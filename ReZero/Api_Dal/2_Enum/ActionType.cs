using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public enum ActionType
    {
        //Query
        QueryBy_PrimaryKey=10000,
        Query_Common = 10001,

        //Insert
        Insert_Object =20000,

        //Delete
        Delete_Object = 30000,

        //Update
        Update_Object = 40000,

        //DDL
        DDL_DatabaseList = 50000
    }
}
