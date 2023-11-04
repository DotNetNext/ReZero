using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    public class DDL_DatabaseList : IDataManager
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            var dataBaseList=db.DbMaintenance.GetDataBaseList(); 
            return await Task.FromResult(dataBaseList);
        }
    }
}
