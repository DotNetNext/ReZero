using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DllGetTables : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var dataBaseList = db.DbMaintenance.GetTableInfoList(false).Where(it=>!it.Name.ToLower().StartsWith("zero_"));
            return await Task.FromResult(dataBaseList);
        }
    }
}