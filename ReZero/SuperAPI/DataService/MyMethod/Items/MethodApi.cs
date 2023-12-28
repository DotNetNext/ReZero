using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public object CompareDatabaseStructure(int[] entityIds)
        {
            return "";
        }

        public object AddOrUpdateEntityColumninfos(string columns)
        {
            try
            {

                List<ZeroEntityColumnInfo> zeroEntityColumns = App.Db.Utilities.DeserializeObject<List<ZeroEntityColumnInfo>>(columns);
                var tableId=zeroEntityColumns.GroupBy(it => it.TableId).Select(it=>it.Key).Single();
                var tableInfo= App.Db.Queryable<ZeroEntityInfo>().Where(it => it.Id == tableId).Single();
                if (tableInfo.IsInitialized) 
                {
                    throw new Exception(TextHandler.GetCommonTexst("系统表不能修改", "The system table cannot be modified"));
                }
                App.Db.Deleteable<ZeroEntityColumnInfo>().Where(it=>it.TableId==tableId).ExecuteCommand();
                foreach (var item in zeroEntityColumns.Where(it=>!string.IsNullOrEmpty(it.PropertyName)))
                {
                    if (string.IsNullOrEmpty(item.DbColumnName) ) 
                    {
                        item.DbColumnName = item.PropertyName;
                    }
                }
                App.Db.Insertable(zeroEntityColumns).ExecuteCommand();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public object  Save
    }
}
