﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class UpdateObject : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitData(type, db, dataModel);
            CheckSystemData(db, dataModel, type, db.EntityMaintenance.GetEntityInfo(type));
            await db.UpdateableByObject(dataModel.Data).UpdateColumns(dataModel.DefaultParameters.Select(it => it.Name).ToArray()).ExecuteCommandAsync();
            return true;
        } 
    }
}
