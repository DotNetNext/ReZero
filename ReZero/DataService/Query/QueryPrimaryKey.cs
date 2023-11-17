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
            var type =await EntityManager.GetTypeAsync(dataModel.TableId);
            var data=await db.QueryableByObject(type).InSingleAsync(dataModel.Data);
            return data;
        }
         
    }
}
