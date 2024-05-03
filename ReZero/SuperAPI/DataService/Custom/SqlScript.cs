using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class SqlScript : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbById(dataModel.DataBaseId) ?? App.Db;
            var sql = dataModel.Sql;
            var pars = new List<SugarParameter>();
            foreach (var item in dataModel.DefaultParameters??new List<DataModelDefaultParameter>())
            {
                var p = new SugarParameter("@"+item.Name,UtilMethods.ConvertDataByTypeName(item.ValueType, item.Value?.ToString()));
                if (item.ValueIsReadOnly) 
                {
                   var claimItem=dataModel.ClaimList.FirstOrDefault(it => it.Key?.ToLower() == item.Name?.ToLower());
                    p = new SugarParameter("@" + item.Name, claimItem.Value);
                }
                pars.Add(p);
            }
            switch (dataModel.ResultType)
            {
                case SqlResultType.DataSet:
                    return await db.Ado.GetDataSetAllAsync(sql, pars);
                case SqlResultType.AffectedRows:
                    return await db.Ado.ExecuteCommandAsync(sql, pars);
                case SqlResultType.Query:
                default: 
                    return await db.Ado.GetDataTableAsync(sql, pars);
            }
        }
    }
}
