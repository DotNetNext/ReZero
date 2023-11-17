using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryByPrimaryKey: IDataService
    {
        private ISqlSugarClient db;
        public QueryByPrimaryKey() 
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
