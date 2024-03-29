﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var isAnyUrl = db.Queryable<ZeroInterfaceList>().Any(it => it!.Url!.ToLower() == url.ToLower());
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
            var interfaceInfos = db.Queryable<ZeroInterfaceList>().ToList();
            var interInfo = interfaceInfos.Where(it => it.Url!.ToLower() == path).FirstOrDefault();
            var dynamicInterfaceContext = new DynamicInterfaceContext() { Context = context };
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
                    dataService.BindHttpParameters.Bind(interInfo.DataModel, context);
                    dynamicInterfaceContext.DataModel= interInfo.DataModel;
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.ISuperApiAop!.OnExecutingAsync(dynamicInterfaceContext);
                    var data = await dataService.ExecuteAction(interInfo.DataModel!);
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.ISuperApiAop!.OnExecutedAsync(dynamicInterfaceContext);
                    var resultModel = interInfo.CustomResultModel ?? new ResultModel();
                    resultModel.OutPutData = interInfo.DataModel?.OutPutData;
                    data = new ResultService().GetResult(data, resultModel);
                    var json = JsonHelper.SerializeObject(data);
                    await context.Response.WriteAsync(json);
                }
                catch (Exception ex)
                {
                    await context.Response.WriteAsync(db.Utilities.SerializeObject(new { message = ex.Message }));
                    await SuperAPIModule._apiOptions!.InterfaceOptions!.ISuperApiAop!.OnErrorAsync(dynamicInterfaceContext);
                }
            }
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
