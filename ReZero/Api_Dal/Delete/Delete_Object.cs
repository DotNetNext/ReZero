using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class Delete_Object : IDataManager
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            await db.DeleteableByObject(dataModel.Data).ExecuteCommandAsync();
            return true;
        }
    }
}
