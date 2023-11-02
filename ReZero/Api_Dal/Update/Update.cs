using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class UpdateObject:IDataManager
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            await db.UpdateableByObject(dataModel.Data).ExecuteCommandAsync();
            return true;
        }
    }
}
