using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System; 

namespace ReZero
{
    /// <summary>
    /// Middleware for handling custom ZeroApi requests.
    /// </summary>
    public class ZeroApiMiddleware
    {
        private readonly IApplicationBuilder _applicationBuilder;

        /// <summary>
        /// Initializes a new instance of the ZeroApiMiddleware class.
        /// </summary>
        /// <param name="application">ASP.NET Core application builder.</param>
        public ZeroApiMiddleware(IApplicationBuilder application)
        {
            _applicationBuilder = application ?? throw new ArgumentNullException(nameof(application));
        }

        /// <summary>
        /// Middleware function to handle custom ZeroApi requests.
        /// </summary>
        /// <returns>A delegate representing the middleware function.</returns>
        public Func<HttpContext, Func<Task>, Task> HandleApiRequests()
        {
            return async (context, next) =>
            {
                // Get the requested URL
                var requestedUrl = context.Request.Path;

                // Implement custom URL request handling logic here
                if (IsZeroDynamicApi(requestedUrl))
                {
                    await ZeroDynamicApi(context);
                }
                else if (IsZeroApi(requestedUrl))
                {
                    await ZeroApi(context);
                }
                else
                {
                    // If the requested URL is not specific, pass the request to the next middleware
                    await next();
                }
            };
        }

        /// <summary>
        /// Writes a custom response for ZeroApi requests.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task ZeroDynamicApi(HttpContext context)
        {
            var app = App.ServiceProvider!.GetService<IDynamicApi>();
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL matches the ZeroApi pattern.
        /// </summary>
        /// <param name="requestedUrl">The requested URL.</param>
        /// <returns>True if the URL is for ZeroApi, otherwise false.</returns>
        private bool IsZeroDynamicApi(PathString requestedUrl)
        {
            var app = App.ServiceProvider!.GetService<IDynamicApi>();
            return app.IsApi(requestedUrl);
        }

        /// <summary>
        /// Writes a custom response for standard ZeroApi requests.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        private async Task ZeroApi(HttpContext context)
        {
            var app = App.ServiceProvider!.GetService<IReZeroApi>();
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL matches the standard ZeroApi pattern.
        /// </summary>
        /// <param name="requestedUrl">The requested URL.</param>
        /// <returns>True if the URL is for standard ZeroApi, otherwise false.</returns>
        private bool IsZeroApi(PathString requestedUrl)
        {
            var app = App.ServiceProvider!.GetService<IReZeroApi>();
            return app.IsApi(requestedUrl);
        }
    }
}