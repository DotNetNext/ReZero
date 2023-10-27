using Microsoft.AspNetCore.Http;
using ReZero.API; 
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
            if (Enum.TryParse<HttpRequestMethod>(requestMethodString, ignoreCase: true, out var requestMethod))
            {
                var handler = helper.GetHandler(requestMethod);
                var result = handler.HandleRequest();
                await context.Response.WriteAsync(result);
            }
            else
            {
                context.Response.StatusCode = 400; // Bad Request
                await context.Response.WriteAsync("Invalid request method");
            }
        }
    }
}
