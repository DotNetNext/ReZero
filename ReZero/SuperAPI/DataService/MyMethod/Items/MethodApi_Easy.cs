using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public partial class MethodApi 
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
        public  object  GetTables(long databaseId)
        { 
            var db = App.GetDbById(databaseId);
            var entitys = App.Db.Queryable<ZeroEntityInfo>()
                .Where(it=>it.IsDeleted==false)
                //.Where(it=>it.IsInitialized==false)
                .Where(it => it.DataBaseId == databaseId).ToList();
            var tables = db!.DbMaintenance.GetTableInfoList(false).Where(it => !it.Name.ToLower().StartsWith("zero_")).ToList(); 
            var result = tables
                            .Where(it=> !entitys.Any(s=>s.DbTableName!.EqualsCase(it.Name))).ToList(); 
            return  result ;
        }
    }
}
