using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class DeleteObject
    {
        public bool Delete(object deleteObject) 
        {
            var db = App.Db;
            db.DeleteableByObject(deleteObject).ExecuteCommand();
            return true;
        }
    }
}
