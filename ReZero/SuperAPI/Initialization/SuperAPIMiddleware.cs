using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http; 
using System;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Middleware class for handling Zero Dynamic API and Internal API requests.
    /// </summary>
    public class SuperAPIMiddleware
    {
        private readonly IApplicationBuilder _applicationBuilder;

        /// <summary>
        /// Constructor for ZeroApiMiddleware class.
        /// </summary>
        /// <param name="application">Instance of IApplicationBuilder.</param>
        public SuperAPIMiddleware(IApplicationBuilder application)
        {
            _applicationBuilder = application ?? throw new ArgumentNullException(nameof(application));
        }

        /// <summary>
        /// Middleware entry point to handle incoming requests.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        /// <param name="next">Delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            // Get the requested URL path from the context
            var requestedUrl = context.Request.Path;

          
            // Check if the requested URL corresponds to Internal API
            if (IsInternalApi(requestedUrl))
            {
                // Handle the request using Internal API logic
                await InternalApi(context);
            }
            // Check if the requested URL corresponds to Dynamic API
            else if(IsDynamicApi(requestedUrl))
            {
                // Handle the request using Dynamic API logic
                await DynamicApi(context);
            }
            // If the requested URL doesn't match any specific API, pass the request to the next middleware
            else
            {
                await next();
            }
        }

        /// <summary>
        /// Handles requests for Dynamic API.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        private async Task DynamicApi(HttpContext context)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL corresponds to Dynamic API.
        /// </summary>
        /// <param name="requestedUrl">Requested URL path.</param>
        /// <returns>True if the URL corresponds to Dynamic API, otherwise false.</returns>
        private bool IsDynamicApi(PathString requestedUrl)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Determine if the requested URL matches Dynamic API
            return app.IsApi(requestedUrl);
        }

        /// <summary>
        /// Handles requests for Internal API.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        private async Task InternalApi(HttpContext context)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL corresponds to Internal API.
        /// </summary>
        /// <param name="requestedUrl">Requested URL path.</param>
        /// <returns>True if the URL corresponds to Internal API, otherwise false.</returns>
        private bool IsInternalApi(PathString requestedUrl)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Determine if the requested URL matches Internal API
            return app.IsApi(requestedUrl);
        }
    }
}