using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    public class DllDatabaseList : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            var dataBaseList=db.DbMaintenance.GetDataBaseList(); 
            return await Task.FromResult(dataBaseList);
        }
    }
}
