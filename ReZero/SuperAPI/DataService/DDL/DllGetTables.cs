using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DllGetTables : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var dataBaseList = db.DbMaintenance.GetTableInfoList(false);
            return await Task.FromResult(dataBaseList);
        }
    }
}