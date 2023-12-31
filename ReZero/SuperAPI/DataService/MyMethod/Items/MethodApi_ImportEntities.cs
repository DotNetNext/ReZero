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
            var db=App.GetDbById(databasdeId)!;
            foreach (var tableName in tableNames)
            {
               var columns= db.DbMaintenance.GetColumnInfosByTableName(tableName);
               var dataTable=db.Queryable<object>().AS(tableName).Where("0="+ PubConst.Random.Next(1,9999999)).ToDataTable();
               var dbColumns = dataTable.Columns.Cast<DataColumn>().ToList();
                
               
            }
            return true;
        }
    }
}
