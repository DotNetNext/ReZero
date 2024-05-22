using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public object ImportEntities(long databasdeId, List<string> tableNames)
        {
            var db = App.GetDbById(databasdeId)!;
            List<ZeroEntityInfo> entityInfos = new List<ZeroEntityInfo>();
            var tableInfos = db.DbMaintenance.GetTableInfoList(false);
            foreach (var tableName in tableNames)
            {
                ZeroEntityInfo entityInfo = CreateEntityInfo(db, tableName,tableInfos); 
                entityInfo.DataBaseId = databasdeId;
                entityInfo.ColumnCount = entityInfo.ZeroEntityColumnInfos?.Count??0;
                entityInfos.Add(entityInfo);
            }
            App.Db.InsertNav(entityInfos).Include(it => it.ZeroEntityColumnInfos).ExecuteCommand();
            return true;
        }

        private  ZeroEntityInfo CreateEntityInfo(SqlSugarClient db, string tableName, List<SqlSugar.DbTableInfo> tableInfos)
        {
            ZeroEntityInfo entityInfo = new ZeroEntityInfo();
            var setting = App.Db.Queryable<ZeroSysSetting>().First(it => it.TypeId == PubConst.Setting_EntityType && it.ChildTypeId == PubConst.Setting_ImportUnunderlineType);
            entityInfo.ClassName = CapitalizeFirstLetter(tableName,setting.BoolValue);
            entityInfo.DbTableName = tableName;
            entityInfo.Description = tableInfos.FirstOrDefault(it => it.Name == tableName)?.Description;
            entityInfo.CreateTime = DateTime.Now;
            var columns = db.DbMaintenance.GetColumnInfosByTableName(tableName, false);
            var dataTable = db.Queryable<object>().AS(tableName).Where(GetWhereFalse()).ToDataTable();
            var dtColumns = dataTable.Columns.Cast<DataColumn>().ToList();
            var joinedColumns = columns.
                Join(dtColumns, c =>
                c.DbColumnName.ToLower(),
                dtc => (dtc.ColumnName?.ToLower()), (c, dtc) =>
                new ZeroEntityColumnInfo
                {
                    DbColumnName = c.DbColumnName,
                    DataType = GetDataType(db.CurrentConnectionConfig.DbType,c),
                    PropertyName = CapitalizeFirstLetter(c.DbColumnName, setting.BoolValue),
                    PropertyType = EntityGeneratorManager.GetNativeTypeByType(GetType(c, dtc)),
                    IsNullable = c.IsNullable,
                    IsPrimarykey = c.IsPrimarykey,
                    IsIdentity = c.IsIdentity,
                    Description = c.ColumnDescription,
                    CreateTime = DateTime.Now
                }).ToList();
            entityInfo.ZeroEntityColumnInfos = joinedColumns;
            return entityInfo;
        }

        private string GetDataType(SqlSugar.DbType dbType, SqlSugar.DbColumnInfo c)
        {
            if (dbType==SqlSugar.DbType.Oracle&&!string.IsNullOrEmpty(c.OracleDataType)) 
            {
                return c.OracleDataType;
            }
            return c.DataType;
        }

        private static Type GetType(SqlSugar.DbColumnInfo c, DataColumn dtc)
        {
            if (dtc.DataType == typeof(string)) 
            {
                return dtc.DataType;
            }
            if (dtc.DataType == typeof(byte[])) 
            {
                return dtc.DataType;
            }
            return c.IsNullable ? typeof(Nullable<>).MakeGenericType(dtc.DataType) : dtc.DataType;
        } 
        #region Helper
        private static string GetWhereFalse()
        {
            return "0=" + PubConst.Common_Random.Next(1, 9999999);
        }
        public string CapitalizeFirstLetter(string input, bool boolValue)
        { 
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            if (boolValue)
            {
                return GetCsharpName(input);
            }
            else
            {
                return char.ToUpper(input[0]) + input.Substring(1);
            }
        }
        public static string GetCsharpName(string dbColumnName)
        {
            if (dbColumnName.Contains("_"))
            {
                dbColumnName = dbColumnName.TrimEnd('_');
                dbColumnName = dbColumnName.TrimStart('_');
                var array = dbColumnName.Split('_').Select(it => GetFirstUpper(it, true)).ToArray();
                return string.Join("", array);
            }
            else
            {
                return GetFirstUpper(dbColumnName);
            }
        }
        private static string GetFirstUpper(string dbColumnName, bool islower = false)
        {
            if (dbColumnName == null)
                return null;
            if (islower)
            {
                return dbColumnName.Substring(0, 1).ToUpper() + dbColumnName.Substring(1).ToLower();
            }
            else
            {
                if (dbColumnName.ToUpper() == dbColumnName)
                {
                    return dbColumnName.Substring(0, 1).ToUpper() + dbColumnName.Substring(1).ToLower();
                }
                else
                {
                    return dbColumnName.Substring(0, 1).ToUpper() + dbColumnName.Substring(1);
                }
            }
        }
        #endregion
    }
}
