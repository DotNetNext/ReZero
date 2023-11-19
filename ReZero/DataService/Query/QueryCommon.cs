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
            var type = await EntityManager.GetTypeAsync(dataModel.TableId);
            var queryObject = db.QueryableByObject(type);
            queryObject = Where(dataModel, queryObject);
            var result = await queryObject
                          .ToPageListAsync(dataModel!.CommonPage!.PageNumber, dataModel.CommonPage.PageSize, count);
            return result;
        }

        private static QueryMethodInfo Where(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.WhereParameters != null)
            {
                foreach (var item in dataModel.WhereParameters)
                {
                     
                }
            } 
            queryObject = queryObject.Where(conditionalModels);
            foreach (var item in funcModels)
            {
                queryObject = queryObject.Where(item);
            }
            return queryObject;
        }
    }
}
