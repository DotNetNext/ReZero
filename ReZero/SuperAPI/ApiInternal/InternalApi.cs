using Microsoft.AspNetCore.Http; 
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Implementation of the ReZero API interface (IReZeroApi) to handle API-related operations.
    /// </summary>
    public class InternalApi : IInternalApi
    {
        /// <summary>
        /// Checks if the provided URL corresponds to a ReZero API endpoint.
        /// </summary>
        /// <param name="url">The URL to be checked.</param>
        /// <returns>True if the URL is a ReZero API endpoint, otherwise false.</returns>
        public bool IsApi(string url)
        {
            return url.ToString().ToLower().TrimStart('/')?.StartsWith(NamingConventionsConst.ApiReZeroRoute.ToLower()) == true;
        }

        /// <summary>
        /// Writes the API response asynchronously to the specified HttpContext.
        /// </summary>
        /// <param name="context">The HttpContext representing the current request and response context.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task WriteAsync(HttpContext context)
        {
            var db = App.Db;
            try
            {
                var path = context.Request.Path.ToString()?.ToLower();
                var interfaceInfos = db.Queryable<ZeroInterfaceList>().ToList();
                var interInfo = interfaceInfos.Where(it => it.Url!.ToLower() == path).FirstOrDefault();

                if (interInfo == null)
                {
                    var message = TextHandler.GetCommonTexst($"未找到内置接口 {path} ，请在表ZeroInterfaceList中查询", $"No built-in interface {path} is found. Query in the table ZeroInterfaceList");
                    await context.Response.WriteAsync(message);
                }
                else
                {
                    DataService dataService = new DataService();
                    dataService.BindHttpParameters.Bind(interInfo.DataModel, context);
                    var data = await dataService.ExecuteAction(interInfo.DataModel ?? new DataModel() { });
                    var resultModel = interInfo.CustomResultModel ?? new ResultModel();
                    resultModel.OutPutData = interInfo.DataModel?.OutPutData;
                    data = new ResultService().GetResult(data, resultModel);
                    await context.Response.WriteAsync(JsonHelper.SerializeObject(data));
                }
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(db.Utilities.SerializeObject(new { message = ex.Message }));
            }
        }
    }
}