using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class SqlScript : CommonDataService, IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            var db = App.GetDbById(dataModel.DataBaseId) ?? App.Db;
            var sql = dataModel.Sql + string.Empty;
            var left = "[[";
            var right = "]]";
            var isWhereIf = sql.Contains(left) && sql.Contains(right);
            var pars = new List<SugarParameter>();
            foreach (var item in dataModel.DefaultParameters ?? new List<DataModelDefaultParameter>())
            {
                var p = new SugarParameter("@" + item.Name, UtilMethods.ConvertDataByTypeName(item.ValueType, item.Value?.ToString()));
                if (item.ValueIsReadOnly)
                {
                    var claimItem = dataModel.ClaimList.FirstOrDefault(it => it.Key?.ToLower() == item.Name?.ToLower());
                    p = new SugarParameter("@" + item.Name, claimItem.Value);
                }
                if (item.ValueType?.Contains(PubConst.Common_ArrayKey) == true)
                {
                    var type = item.ValueType.Replace(PubConst.Common_ArrayKey, string.Empty);
                    var arrayType = typeof(List<>).MakeGenericType(EntityGeneratorManager.GetTypeByString(type));
                    var value = JsonConvert.DeserializeObject(item.Value?.ToString() ?? PubConst.Common_ArrayKey, arrayType);
                    p = new SugarParameter("@" + item.Name, value);
                }
                sql = GetSqlByIsWhereIF(sql, left, right, isWhereIf, p);
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

        private static string GetSqlByIsWhereIF(string sql, string left, string right, bool isWhereIf, SugarParameter p)
        {
            if (isWhereIf)
            {
                var regex = @"\[\[.*?\]\]";
                var matchCollection = Regex.Matches(sql, regex);
                foreach (Match math in matchCollection)
                {
                    var value = math.Value;
                    if (value.Contains(p.ParameterName) && string.IsNullOrEmpty(p.Value?.ToString()))
                    {
                        sql = sql.Replace(value, string.Empty);
                    }
                    else if (value.Contains(p.ParameterName) && !string.IsNullOrEmpty(p.Value?.ToString()))
                    {
                        sql = sql.Replace(value, value.Replace(left, null).Replace(right, null));
                    }
                }
            }

            return sql;
        }
         
    }
}
