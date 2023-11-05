using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero
{
    public class DynamicApiManager : IDynamicApi
    {
        public bool IsApi(string url)
        {
            var db = App.Db;
            var isAnyUrl = db.Queryable<ZeroInterfaceList>().Any(it => it!.Url!.ToLower() == url.ToLower());
            return false;
        }

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
        private static async Task WriteAsyncSuccess(HttpContext context, DynamicApiHelper helper, HttpRequestMethod requestMethod)
        {
            var handler = helper.GetHandler(requestMethod,context);
            var result = handler.HandleRequest();
            await context.Response.WriteAsync("");
        }
        private static async Task WriteError(HttpContext context)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid request method");
        } 
    }
}
