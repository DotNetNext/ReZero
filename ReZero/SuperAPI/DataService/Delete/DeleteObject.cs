using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class DeleteObject : CommonDataService,IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            //CheckSystemData(db, dataModel, type, db.EntityMaintenance.GetEntityInfo(type));
            base.InitData(type,db, dataModel);
            await db.DeleteableByObject(dataModel.Data).ExecuteCommandAsync();

            base.ClearAll(dataModel);
            return true;
        }
    }
}
