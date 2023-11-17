using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryCommon : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.Db;
            RefAsync<int> count = 0;
            var type = GetType(dataModel.TableId);
            //var entityInfo = db.EntityMaintenance.GetEntityInfo(type);
            var result = await db.QueryableByObject(type)
                          .ToPageListAsync(dataModel!.CommonPage!.PageNumber, dataModel.CommonPage.PageSize, count);
            return result;
        }

        private static Type GetType(long tableId)
        {
            return  null;
        }
    }
}
