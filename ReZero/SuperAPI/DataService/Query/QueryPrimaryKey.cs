using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class QueryByPrimaryKey:CommonDataService, IDataService
    {
       
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type =await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            var id = dataModel.DefaultParameters.First().Value;
            var data=await db.QueryableByObject(type).InSingleAsync(id);
            return data;
        }
         
    }
}
