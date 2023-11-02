using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class QueryObject
    {
        private ISqlSugarClient db;
        public QueryObject() 
        {
            db = App.Db;
        }
        public bool QuerySingle(Type type,object Id)
        {
            
            //SqlSugar 缺少Type.InSingle
            //db.QueryableByObject(type).Where();
            return true;
        }

        public bool QueryPage(Type type,CommonPage commonPage)
        {
           
            var count = 0;
            var result= db.QueryableByObject(type)
                          .ToPageList(commonPage.PageNumber, commonPage.PageSize,ref count);
            return true;
        }
        public bool QueryAll(Type type, CommonPage commonPage)
        {
            var db = App.Db;
            var count = 0;
            var result = db.QueryableByObject(type)
                          .ToPageList(commonPage.PageNumber, commonPage.PageSize, ref count);
            return true;
        }
    }
}
