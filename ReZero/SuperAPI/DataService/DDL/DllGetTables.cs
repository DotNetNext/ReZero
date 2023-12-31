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
            var dbId =Convert.ToInt32(dataModel.DefaultParameters.First().Value);
            var db = App.GetDbById(dbId);
            var dataBaseList = db!.DbMaintenance.GetTableInfoList(false).Where(it=>!it.Name.ToLower().StartsWith("zero_"));
            return await Task.FromResult(dataBaseList);
        }
    }
}