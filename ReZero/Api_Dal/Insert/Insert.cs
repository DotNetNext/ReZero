using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class InsertObject
    {
        public bool Insert(object InsertObject)
        {
            var db = App.Db;
            db.InsertableByObject(InsertObject).ExecuteCommand();
            return true;
        }
    }
}
