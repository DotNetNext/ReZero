using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ReZero
{
    /// <summary>
    /// Core library for the ReZero framework.
    /// </summary>
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Initializes ReZero framework with the provided options.
        /// </summary>
        /// <param name="app">The Application Builder.</param>
        /// <param name="options">The ReZero options to configure the framework.</param>
        public static void ReZeroInit(this IApplicationBuilder app, ReZeroOptions? options=null)
        {
            // Create an instance of ZeroApiMiddleware
            ZeroApiMiddleware zeroApi = new ZeroApiMiddleware(app);

            // Use ZeroApiMiddleware to handle API requests
            app.Use(zeroApi.HandleApiRequests());
        }
    }
}