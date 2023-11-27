using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class Update_Object : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            await db.UpdateableByObject(dataModel.Data).ExecuteCommandAsync();
            return true;
        }
    }
}
