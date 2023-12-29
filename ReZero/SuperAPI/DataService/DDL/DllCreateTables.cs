using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class DllCreateTables : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            await Task.Delay(0);
            var value = dataModel.DefaultParameters.First().Value; 
            var dbRoot = App.Db;
            var ids = dbRoot.Utilities.DeserializeObject<List<long>>(value+"");
            List<string> tableDifferences = new List<string>();
            var result = string.Empty;
            var entities = dbRoot.Queryable<ZeroEntityInfo>().In(ids).ToList();
            foreach (var entity in entities)
            {
                var codeFirstDb = App.GetDbById(entity.DataBaseId) ?? App.Db;
                var type = EntityGeneratorManager.GetTypeAsync(entity.Id).GetAwaiter().GetResult();
                 codeFirstDb.CodeFirst.InitTables(type);
            }
            return true;
        }
    }
}