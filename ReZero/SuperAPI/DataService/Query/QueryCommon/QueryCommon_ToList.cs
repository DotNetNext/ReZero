using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
namespace ReZero.SuperAPI
{
    public partial class QueryCommon : IDataService
    {
        private static async Task<object?> ToList(DataModel dataModel, RefAsync<int> count, Type type, QueryMethodInfo queryObject)
        {
            object? result = null;
            if (IsDefault(dataModel))
            {
                result = await DefaultQuery(queryObject, result);
            }
            else
            {
                result = await PageQuery(dataModel, count, type, queryObject, result);

            }
            return result;
        }
    }
}
