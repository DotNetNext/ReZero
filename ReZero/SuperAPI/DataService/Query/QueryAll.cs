 using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class QueryAll :CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db; 
            RefAsync<int> count = 0;
            var parameter = dataModel.TreeParameter;
            var type =await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            var result = await db.QueryableByObject(type)
                          .ToListAsync();
            return result;
        }
    }
}
