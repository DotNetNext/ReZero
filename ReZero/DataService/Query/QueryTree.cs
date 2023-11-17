using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryTree : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            RefAsync<int> count = 0;
            var parameter = dataModel.TreeParameter;
            var type =await EntityManager.GetTypeAsync(dataModel.TableId);
            var result = await db.QueryableByObject(type)
                          .ToTreeAsync(parameter?.ChildPropertyName,parameter?.ParentCodePropertyName,parameter?.RootValue,parameter?.CodePropertyName);
            return result;
        }
    }
}
