using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    internal class QueryObject
    {
        private ISqlSugarClient db;
        public QueryObject() 
        {
            db = App.Db;
        }
        public Task<object> QuerySingleAsync(Type type,object primaryKeyValue)
        { 
            var data= db.QueryableByObject(type).InSingleAsync(primaryKeyValue);
            return data;
        }

        public async Task<object> QueryPage(Type type,CommonPage commonPage)
        {
           
            RefAsync<int> count = 0;
            var result=await db.QueryableByObject(type)
                          .ToPageListAsync(commonPage.PageNumber, commonPage.PageSize, count);
            return result;
        }
        public async Task<object> QueryAll(Type type, CommonPage commonPage)
        {
            var db = App.Db;
            var count = 0;
            var result =await db.QueryableByObject(type)
                          .ToPageListAsync(commonPage.PageNumber, commonPage.PageSize,  count);
            return true;
        }
    }
}
