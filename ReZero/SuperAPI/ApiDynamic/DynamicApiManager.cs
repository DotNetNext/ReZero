using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DynamicApiManager : IDynamicApi
    {
        /// <summary>
        /// Determines if the given URL is a valid API endpoint.
        /// </summary>
        /// <param name="url">The URL to check.</param>
        /// <returns>True if the URL is a valid API endpoint, false otherwise.</returns>
        public bool IsApi(string url)
        {
            var db = App.Db;
            var isAnyUrl = CacheManager<ZeroInterfaceList>.Instance.GetList()
                .Any(it =>
                it!.Url!.ToLower() == url.ToLower()||
                (it.OriginalUrl!=null&&url.ToLower().StartsWith(it.OriginalUrl.ToLower()))
                );
            return isAnyUrl;
        }

        /// <summary>
        /// Writes the response for the given HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        public async Task WriteAsync(HttpContext context)
        {
            var helper = new DynamicApiHelper();
            var requestMethodString = context.Request.Method;
            HttpRequestMethod requestMethod;
            if (helper.IsHttpMethod(requestMethodString, out requestMethod))
            {
                await WriteAsyncSuccess(context, helper, requestMethod);
            }
            else
            {
                await WriteError(context);
            }
        }

        /// <summary>
        /// Writes the response for a successful API request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="helper">The dynamic API helper.</param>
        /// <param name="requestMethod">The HTTP request method.</param>
        private static async Task WriteAsyncSuccess(HttpContext context, DynamicApiHelper helper, HttpRequestMethod requestMethod)
        {
            var handler = helper.GetHandler(requestMethod, context);
            var db = App.Db;
            var path = context.Request.Path.ToString()?.ToLower(); 
            var interInfo = CacheManager<ZeroInterfaceList>
                                             .Instance.GetList()
                                             .Where(it =>
                                                     it.Url!.ToLower() == path
                                                     ||
                                                     (it.OriginalUrl != null && path!.ToLower().StartsWith(it.OriginalUrl.ToLower()))
                                                    )?.First();
            interInfo=db.Utilities.TranslateCopy(interInfo);
            var dynamicInterfaceContext = new InterfaceContext() {  InterfaceType= InterfaceType.DynamicApi,HttpContext = context,InterfaceInfo=interInfo };
            if (interInfo == null)
            {
                var message = TextHandler.GetCommonText($"未找到内置接口 {path} ，请在表ZeroInterfaceList中查询", $"No built-in interface {path} is found. Query in the table ZeroInterfaceList");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(message);
            }
            else
            {
                try
                {
                    DataService dataService = new DataService();
                    interInfo!.DataModel!.ApiId = interInfo.Id;
                    interInfo!.DataModel!.ResultType = interInfo.DataModel?.ResultType;
                    interInfo!.DataModel!.Sql = interInfo.DataModel?.Sql;
                    interInfo!.DataModel!.DataBaseId = interInfo.DataModel?.DataBaseId ?? 0;
                    dataService.BindHttpParameters.Bind(interInfo.DataModel, context,path,!string.IsNullOrEmpty(interInfo.OriginalUrl),interInfo);
                    dynamicInterfaceContext.DataModel = interInfo.DataModel;
                    var service = DependencyInjection.DependencyResolver.Provider;
                    dynamicInterfaceContext.ServiceProvider = service;
                    interInfo.DataModel!.ServiceProvider = service;
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.SuperApiAop!.OnExecutingAsync(dynamicInterfaceContext);
                    await InstanceManager.AuthorizationAsync(context, dynamicInterfaceContext);
                    var data = await dataService.ExecuteAction(interInfo.DataModel!);
                    data = GetUserInfo(path, interInfo, data);
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.SuperApiAop!.OnExecutedAsync(dynamicInterfaceContext);
                    var resultModel = interInfo.CustomResultModel ?? new ResultModel();
                    resultModel.OutPutData = interInfo.DataModel?.OutPutData;
                    data = new ResultService().GetResult(data!, resultModel);
                    data = SuperAPIModule._apiOptions?.InterfaceOptions?.MergeDataToStandardDtoFunc?.Invoke(data) ?? data;
                    var json = JsonHelper.SerializeObject(data, SuperAPIModule._apiOptions!.InterfaceOptions?.JsonSerializerSettings);
                    context.Response.ContentType = PubConst.DataSource_ApplicationJson;
                    await context.Response.WriteAsync(json);
                }
                catch (Exception ex)
                {
                    ReZero.DependencyInjection.DependencyResolver.GetService<ILogger<SuperAPIMiddleware>>().LogInformation(ex.Message);
                    object data =new ErrorResponse { message = ex.Message } ;
                    data=SuperAPIModule._apiOptions?.InterfaceOptions?.MergeDataToStandardDtoFunc?.Invoke(data)??data;
                    context.Response.ContentType = PubConst.DataSource_ApplicationJson;
                    await context.Response.WriteAsync(db.Utilities.SerializeObject(data));
                    dynamicInterfaceContext.Exception = ex;
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.SuperApiAop!.OnErrorAsync(dynamicInterfaceContext);
                }
            }
        }

        private static object? GetUserInfo(string? path, ZeroInterfaceList interInfo, object? data)
        {
            if (path == PubConst.Jwt_GetJwtInfo)
            { 
                data = interInfo?.DataModel?.ClaimList;
                if (interInfo?.DataModel?.ClaimList?.Any()!=true)
                {
                    throw new Exception(TextHandler.GetCommonText("你没有启用JWT授权或者没有配置Claim", "You did not enable JWT authorization or did not configure Claim"));
                }
            } 
            return data;
        }


        /// <summary>
        /// Writes the response for an invalid API request.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        private static async Task WriteError(HttpContext context)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid request method");
        }
    }
}
