using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class MethodApi
    {
        public bool TestDb(long Id)
        {
            SqlSugarClient? db = App.GetDbById(Id);
            if (db == null)
            {
                return false;
            }
            else 
            {
                return db.Ado.IsValidConnection();
            }
        }

        public object CreateDb(long dbId)
        {
            try
            {
                SqlSugarClient? db = App.GetDbById(dbId);
                db!.DbMaintenance.CreateDatabase();
                return true;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
