 using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class QueryTree : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            RefAsync<int> count = 0;
            var parameter = dataModel.TreeParameter;
            var type =await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            var data = await db.QueryableByObject(type)
                .InSingleAsync(dataModel.DefaultParameters.First().Value);
            object? parentId = 1;
            if(data!=null)
                parentId=data.GetType()?.GetProperty(parameter?.ParentCodePropertyName)?.GetValue(data)??1;
            var result = await db.QueryableByObject(type)
                          .ToTreeAsync(parameter?.ChildPropertyName,parameter?.ParentCodePropertyName, parentId , parameter?.CodePropertyName);
            return result;
        }
    }
}
