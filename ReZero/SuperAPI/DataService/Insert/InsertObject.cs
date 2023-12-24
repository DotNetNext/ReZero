using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class InsertObject: CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db=App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitData(type,db,dataModel);
            await db.InsertableByObject(dataModel.Data).ExecuteCommandAsync();
            return true;
        }
    }
}
