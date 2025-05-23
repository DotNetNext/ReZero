using Newtonsoft.Json;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
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
                if (IsDateOnly(item))
                {
                    p.DbType = System.Data.DbType.Date;
                    p.Value = Convert.ToDateTime(p.Value);
                }
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
                case SqlResultType.PageQuery:
                    return await GenerateResultPageGrid(db, sql, pars,dataModel);
                case SqlResultType.Query:
                default:
                    return await db.Ado.GetDataTableAsync(sql, pars);
            }
        }

        private static async Task<object> GenerateResultPageGrid(ISqlSugarClient db, string sql, List<SugarParameter> pars, DataModel dataModel)
        { 
            RefAsync<int> count = 0;
            int pageNumber = dataModel.DefaultParameters?.FirstOrDefault(it => it.Name == PubConst.Common_Sql_PageNumber)?.Value?.ObjToInt() ?? PubConst.Common_Sql_PageNumberDefaultValue;
            int pageSize = dataModel.DefaultParameters?.FirstOrDefault(it => it.Name == PubConst.Common_Sql_PageSize)?.Value?.ObjToInt() ?? PubConst.Common_Sql_PageSizeDefaultValue;
            if (pageNumber == 0) pageSize = PubConst.Common_Sql_PageNumberDefaultValue;
            if (pageSize == 0) pageSize = PubConst.Common_Sql_PageSizeDefaultValue;
            var array = Regex.Split(sql, @" order[ ]+by ",RegexOptions.IgnoreCase);
            DataTable dt = null!;
            if (array.Count() == 2)
            {
                dt = await db.SqlQueryable<object>(array.First()).OrderBy(array.Last()).AddParameters(pars).ToDataTablePageAsync(pageNumber, pageSize, count);
            }
            else
            {
                dt = await db.SqlQueryable<object>(sql).AddParameters(pars).ToDataTablePageAsync(pageNumber, pageSize, count);
            }
            return new ResultPageGrid
            {
                Data = dt,
                Columns = dt.Columns.Cast<DataColumn>().Select(col => new ResultGridColumn
                {
                    ColumnDescription = col.ColumnName,
                    PropertyName = col.ColumnName
                }),
                Page = new ResultPage()
                {
                    TotalCount = count.Value,
                    PageNumber=pageNumber,
                    PageSize = pageSize,
                    TotalPage = (int)Math.Ceiling((double)count / pageSize)
                }
            }; 
        }

        private static bool IsDateOnly(DataModelDefaultParameter item)
        {
            return item?.ValueType?.EqualsCase("DateOnly") == true;
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
