using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI 
{
    internal class InsertRange : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            this.InitData(type, db, dataModel);
            this.SetDefaultValue(dataModel, db, type);
            await db.InsertableByObject(dataModel.Data).ExecuteCommandAsync(); 
            base.ClearAll(dataModel);
            return true;
        }
        internal new void InitData(Type type, ISqlSugarClient db, DataModel dataModel)
        {
            var json = dataModel?.DefaultParameters?.FirstOrDefault().Value + "";
            object obj = JsonConvert.DeserializeObject(json,typeof(List<>).MakeGenericType(type))!;
            dataModel!.Data = obj;
        }
        private void SetDefaultValue(DataModel dataModel, ISqlSugarClient db, Type type)
        {
             
        }
    }
}
