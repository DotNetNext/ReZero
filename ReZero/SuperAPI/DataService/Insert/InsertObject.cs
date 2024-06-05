using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ReZero.SuperAPI
{
    internal class InsertObject: CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        { 
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            base.InitData(type, db, dataModel);
            this.SetDefaultValue(dataModel, db, type);
            await db.InsertableByObject(dataModel.Data).ExecuteCommandAsync();

            base.ClearAll(dataModel);
            return true;
        }

        private  void SetDefaultValue(DataModel dataModel, ISqlSugarClient db, Type type)
        {
            if (EntityMappingService.IsAnyDefaultValue(dataModel))
            {
                dataModel.Data = EntityMappingService.GetDataByDefaultValueParameters(type, db, dataModel);
            }
        }

    }
}
