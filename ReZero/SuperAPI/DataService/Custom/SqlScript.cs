using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class SqlScript : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbTableId(dataModel.TableId) ?? App.Db;
            var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
            base.InitDb(type, db);
            base.InitData(type, db, dataModel);
            return true;
        }
    }
}
