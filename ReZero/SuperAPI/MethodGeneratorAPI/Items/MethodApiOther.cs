using ReZero.Excel;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.DirectoryServices.Protocols;
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
        public object GetImportTables(long databaseId, string tableName)
        {
            var db = App.GetDbById(databaseId);
            var entitys = App.Db.Queryable<ZeroEntityInfo>()
                .Where(it => it.IsDeleted == false)
                .Where(it => it.DataBaseId == databaseId).ToList();
            var tables = db!.DbMaintenance.GetTableInfoList(false).Where(it => !it.Name.ToLower().StartsWith("zero_")).ToList();
            var result = tables
                            .OrderBy(it => it.Name)
                            .Where(it => !entitys.Any(s => s.DbTableName!.EqualsCase(it.Name))).ToList();
            if (!string.IsNullOrEmpty(tableName))
            {
                result = result.Where(it => it.Name.ToLower().Contains(tableName.ToLower())).ToList();
            }
            return result;
        }
        public object GetUserInfo()
        {
            return null;
        }
        public object GetTables(long databaseId, string tableName)
        {
            var db = App.GetDbById(databaseId);
            var entitys = App.Db.Queryable<ZeroEntityInfo>()
                .Where(it => it.IsDeleted == false)
                .WhereIF(!string.IsNullOrEmpty(tableName), it => it.DbTableName!.ToLower().Contains(tableName.ToLower()))
                .Where(it => it.DataBaseId == databaseId).ToList()
                .Where(it => !it.DbTableName!.ToLower().StartsWith("zero_"));
            var result = entitys.Select(it => new DbTableInfo()
            {
                Id = it.Id,
                Name = it.ClassName,
                Description = it.Description
            }).ToList();
            return result;
        }

        public object ExecuetSql(long databaseId, string sql)
        {
            var db = App.GetDbById(databaseId);
            sql = sql + string.Empty;
            if (db!.CurrentConnectionConfig.DbType == SqlSugar.DbType.Oracle && sql.Contains(";") && !sql.ToLower().Contains("begin"))
            {
                var sqls = sql.Split(';');
                List<object> result = new List<object>();
                foreach (var item in sqls)
                {
                    if (!string.IsNullOrEmpty(item.Trim().Replace("\r", "").Replace("\n", "")))
                    {
                        result.Add(GetObject(item, db));
                    }
                }
                if (result.Count == 1)
                {
                    return result.FirstOrDefault();
                }
                else
                {
                    return result;
                }
            }
            else
            {
                var result = GetObject(sql, db);
                return result;
            }
        }
         
        public byte[] ExecuetSqlReturnExcel(long databaseId, string sql)
        {
            var db = App.GetDbById(databaseId);
            sql = sql + string.Empty; 
            DataSet result = new DataSet();
            if (db!.CurrentConnectionConfig.DbType == SqlSugar.DbType.Oracle && sql.Contains(";") && !sql.ToLower().Contains("begin"))
            {
                var sqls = sql.Split(';');
                foreach (var item in sqls)
                {
                    if (!string.IsNullOrEmpty(item.Trim().Replace("\r", "").Replace("\n", "")))
                    {
                        result.Tables.Add(db.Ado.GetDataTable(sql));
                    }
                } 
            }
            else
            {
               result = db!.Ado.GetDataSetAll(sql);  
            }
            var bytes=  DataTableToExcel.ExportExcel(result, nameof(ExecuetSqlReturnExcel));
            return bytes;
        }

        private static object GetObject(string sql, SqlSugarClient? db)
        {
            if (sql.ToLower().Contains("select"))
            {
                var ds = db!.Ado.GetDataSetAll(sql);
                if (ds.Tables.Count == 1)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return ds;
                }
            }
            else if (db!.CurrentConnectionConfig.DbType == SqlSugar.DbType.SqlServer && sql.ToLower().Contains("go"))
            {
                return db!.Ado.ExecuteCommandWithGo(sql);
            }
            else
            {
                return db!.Ado.ExecuteCommand(sql) + " affected rows";
            }
        }


        public static bool ClearAllInternalCache()
        {
            var cc = new CacheCenter();
            cc.ClearAllInternalCache();
            return true;
        }
    }
}
