using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
namespace ReZero.SuperAPI 
{
    public partial class QueryCommon : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            try
            {
                var db = App.Db;
                RefAsync<int> count = 0;
                var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
                var queryObject = db.QueryableByObject(type, "t0");
                queryObject = Join(dataModel, queryObject);
                queryObject = Where(dataModel, queryObject);
                queryObject = OrderBy(dataModel, queryObject);
                object? result = await ToList(dataModel, count, type, queryObject);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        } 
    } 
}
