using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class DeleteObject
    {
        public async Task<bool> Delete(object deleteObject) 
        {
            var db = App.Db;
            await db.DeleteableByObject(deleteObject).ExecuteCommandAsync();
            return true;
        }
    }
}
