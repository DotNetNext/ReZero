using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryByPrimaryKey: IDataManager
    {
        private ISqlSugarClient db;
        public QueryByPrimaryKey() 
        {
            db = App.Db;
        }
        public async Task<object> ExecuteAction(DataModel dataModel)
        { 
            var data= db.QueryableByObject(dataModel.MasterEntityType).InSingleAsync(dataModel.Data);
            return data;
        }

      
        public async Task<object> QueryAll(Type type, CommonPage commonPage)
        {
            var db = App.Db;
            var count = 0;
            var result =await db.QueryableByObject(type)
                          .ToPageListAsync(commonPage.PageNumber, commonPage.PageSize,  count);
            commonPage.Total = count;
            return true;
        }
    }
}
