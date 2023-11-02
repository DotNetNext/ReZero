using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class UpdateObject
    {
        public async Task<bool> Update(object updateObject)
        {
            var db = App.Db;
            await db.UpdateableByObject(updateObject).ExecuteCommandAsync();
            return true;
        }
    }
}
