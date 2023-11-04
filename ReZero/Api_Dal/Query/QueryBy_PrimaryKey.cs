using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryBy_PrimaryKey: IDataManager
    {
        private ISqlSugarClient db;
        public QueryBy_PrimaryKey() 
        {
            db = App.Db;
        }
        public async Task<object> ExecuteAction(DataModel dataModel)
        { 
            var data=await db.QueryableByObject(dataModel.MasterEntityType).InSingleAsync(dataModel.Data);
            return data;
        }
         
    }
}
