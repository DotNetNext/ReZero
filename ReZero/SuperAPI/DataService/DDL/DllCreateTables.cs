using SqlSugar;
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
                if (entity.IsInitialized) 
                {
                    throw new Exception(TextHandler.GetCommonText("系统表不能修改", "The system table cannot be modified"));
                }
                var codeFirstDb = App.GetDbTableId(entity.Id)!;
                var type = EntityGeneratorManager.GetTypeAsync(entity.Id).GetAwaiter().GetResult();
                var entityInfo = codeFirstDb.EntityMaintenance.GetEntityInfo(type);
                if (entityInfo.Columns.Any(it => !string.IsNullOrEmpty(it.DataType))) 
                {
                    codeFirstDb.CurrentConnectionConfig.MoreSettings.SqlServerCodeFirstNvarchar = false;
                }
                codeFirstDb.CodeFirst.InitTables(type);
                codeFirstDb.CurrentConnectionConfig.MoreSettings.SqlServerCodeFirstNvarchar = true;
            }
            return true;
        }
    }
}