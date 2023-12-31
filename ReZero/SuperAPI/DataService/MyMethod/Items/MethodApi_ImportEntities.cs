using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public object ImportEntities(long databasdeId, List<string> tableNames)
        {
            var db = App.GetDbById(databasdeId)!;
            List<EntityInfo> entityInfos = new List<EntityInfo>();
            foreach (var tableName in tableNames)
            {
                EntityInfo entityInfo = CreateEntityInfo(db, tableName); 
                entityInfos.Add(entityInfo);
            }
            return true;
        }

        private static EntityInfo CreateEntityInfo(SqlSugarClient db, string tableName)
        {
            EntityInfo entityInfo = new EntityInfo();
            var columns = db.DbMaintenance.GetColumnInfosByTableName(tableName, false);
            var dataTable = db.Queryable<object>().AS(tableName).Where(GetWhereFalse()).ToDataTable();
            var dtColumns = dataTable.Columns.Cast<DataColumn>().ToList();
            var joinedColumns = columns.
                Join(dtColumns, c =>
                c.TableName,
                dtc => dtc.ColumnName, (c, dtc) =>
                new
                {
                    ColumnName = c.TableName,
                    DataType = c.DataType,
                    PropetyInfo = dtc.DataType,
                    IsNullable = c.IsNullable
                });
            List<ZeroEntityColumnInfo> zeroEntityColumns = new List<ZeroEntityColumnInfo>();
            foreach (var item in joinedColumns)
            {
                ZeroEntityColumnInfo zeroEntityColumnInfo = CreateEntityColumn(item);
                zeroEntityColumns.Add(zeroEntityColumnInfo);
            }
            return entityInfo;
        }
        private static ZeroEntityColumnInfo CreateEntityColumn(object item)
        {
            ZeroEntityColumnInfo zeroEntityColumnInfo = new ZeroEntityColumnInfo();
            return zeroEntityColumnInfo;
        }

        #region Helper
        private static string GetWhereFalse()
        {
            return "0=" + PubConst.Random.Next(1, 9999999);
        }

        #endregion
    }
}
