using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
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
            var type =await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            var id = dataModel.WhereParameters.First().Value;
            var data=await db.QueryableByObject(type).InSingleAsync(id);
            return data;
        }
         
    }
}
