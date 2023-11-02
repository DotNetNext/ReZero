using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class UpdateObject
    {
        public bool Insert(object updateObject)
        {
            var db = App.Db;
            db.UpdateableByObject(updateObject).ExecuteCommand();
            return true;
        }
    }
}
