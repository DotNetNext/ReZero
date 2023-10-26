using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ReZero
{
    public class ZeroApiMiddleware
    {
        private readonly IApplicationBuilder _applicationBuilder;

        /// <summary>
        /// nitialize a new instance of ZeroApiMiddleware.
        /// </summary>
        /// <param name="application">ASP.NET Core application Generator.</param>
        public ZeroApiMiddleware(IApplicationBuilder application)
        {
            _applicationBuilder = application ?? throw new ArgumentNullException(nameof(application));
        } 

        /// <summary>
        /// Middleware function to handle custom ZeroApi requests.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="next">The next middleware delegate.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public Func<HttpContext, Func<Task>, Task> HandleApiRequests()
        {
            return async (context, next) =>
            {
                // Get the requested URL
                var requestedUrl = context.Request.Path;

                // Implement custom URL request handling logic here
                if (IsZeroApi(requestedUrl))
                {
                    await WriteZeroApiResponse(context);
                }
                else
                {
                    // If the requested URL is not specific, pass the request to the next middleware
                    await next();
                }
            };
        }

        /// <summary>
        /// Checks if the requested URL matches the ZeroApi pattern.
        /// </summary>
        /// <param name="requestedUrl">The requested URL.</param>
        /// <returns>True if the URL is for ZeroApi, otherwise false.</returns>
        private bool IsZeroApi(PathString requestedUrl)
        {
            var app= App.ServiceProvider.GetService<IApi>();
            return app.IsApi(requestedUrl);
        }

        /// <summary>
        /// Writes a custom response for ZeroApi requests.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task WriteZeroApiResponse(HttpContext context)
        {
            var app = App.ServiceProvider.GetService<IApi>();
            await app.WriteAsync(context);
        }
    }
}