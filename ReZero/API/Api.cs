using Microsoft.AspNetCore.Http;
using ReZero.API;
using ReZero.API.Enum;
using ReZero.API.RequestHandler;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero
{
    public class Api : IApi
    {
        public bool IsApi(string url)
        {
            return false;
        }

        public async Task WriteAsync(HttpContext context)
        {
            var helper = new ApiHelper();
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
        private static async Task WriteAsyncSuccess(HttpContext context, ApiHelper helper, HttpRequestMethod requestMethod)
        {
            var handler = helper.GetHandler(requestMethod);
            var result = handler.HandleRequest();
            await context.Response.WriteAsync(result);
        }
        private static async Task WriteError(HttpContext context)
        {
            context.Response.StatusCode = 400; // Bad Request
            await context.Response.WriteAsync("Invalid request method");
        } 
    }
}
