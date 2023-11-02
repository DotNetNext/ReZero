using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class InsertObject
    {
        public async Task<bool> Insert(object InsertObject)
        {
            var db = App.Db;
            await db.InsertableByObject(InsertObject).ExecuteCommandAsync();
            return true;
        }
    }
}
