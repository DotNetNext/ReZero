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
            if (IsAnyDefaultValue(dataModel))
            {
                dataModel.Data =EntityMappingService.GetDataByDefaultValueParameters(type,db,dataModel);
            }
            await db.InsertableByObject(dataModel.Data).ExecuteCommandAsync();
            return true;
        }

        private static bool IsAnyDefaultValue(DataModel dataModel)
        {
            return dataModel.DefaultValueColumns?.Any() == true;
        } 
    }
}
