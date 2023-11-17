using Microsoft.AspNetCore.Http; 
using System;
using System.Threading.Tasks;

namespace ReZero
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
           //
           await context.Response.WriteAsync("动态接口成功");
        }
    }
}