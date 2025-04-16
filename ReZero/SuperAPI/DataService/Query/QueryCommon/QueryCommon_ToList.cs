using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
using System.Collections;
using ReZero.DependencyInjection;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// ToList
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private  async Task<object?> ToList(DataModel dataModel, RefAsync<int> count, Type type, QueryMethodInfo queryObject)
        {
            object? result = null;
            if (IsDefaultToList(dataModel))
            {
                result = await DefaultQuery(queryObject, result);
                if (dataModel.ApiId == InterfaceListInitializerProvider.IntIntListId) 
                {
                    var userName = DependencyResolver.GetLoggedInUser();
                    var list = CacheManager<ZeroPermissionInfo>.Instance.GetList();
                    if (list.Any())
                    {
                        var mappings = CacheManager<ZeroPermissionMapping>.Instance.GetList()
                            .Where(it => it.UserName!.ToLower() == userName?.ToLower())
                            .ToList();
                        var ids = mappings.Select(it => it.InterfaceId).ToList();
                        result = (result as IList).Cast<object>()
                            .Where(it=> ids.Contains(Convert.ToInt64(it.GetType().GetProperty(nameof(ZeroInterfaceList.Id)).GetValue(it)))).ToList();
                    }
                } 
            }
            else
            {
                result = await PageQuery(dataModel, count, type, queryObject, result);

            }
            return result;
        }
    }
}
