using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ReZero
{
    // Middleware class for handling Zero Dynamic API and Internal API requests
    public class ZeroApiMiddleware
    {
        private readonly IApplicationBuilder _applicationBuilder;

        // Constructor for ZeroApiMiddleware class
        public ZeroApiMiddleware(IApplicationBuilder application)
        {
            _applicationBuilder = application ?? throw new ArgumentNullException(nameof(application));
        }

        // Middleware entry point to handle incoming requests
        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            // Get the requested URL path from the context
            var requestedUrl = context.Request.Path;

            // Check if the requested URL corresponds to Zero Dynamic API
            if (IsDynamicApi(requestedUrl))
            {
                // Handle the request using Zero Dynamic API logic
                await DynamicApi(context);
            }
            // Check if the requested URL corresponds to Internal API
            else if (IsInternalApi(requestedUrl))
            {
                // Handle the request using Internal API logic
                await InternalApi(context);
            }
            // If the requested URL doesn't match any specific API, pass the request to the next middleware
            else
            {
                await next();
            }
        }

        // Handles requests for Dynamic API
        private async Task DynamicApi(HttpContext context)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        // Checks if the requested URL corresponds to Dynamic API
        private bool IsDynamicApi(PathString requestedUrl)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Determine if the requested URL matches Dynamic API
            return app.IsApi(requestedUrl);
        }

        // Handles requests for Internal API
        private async Task InternalApi(HttpContext context)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        // Checks if the requested URL corresponds to Internal API
        private bool IsInternalApi(PathString requestedUrl)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Determine if the requested URL matches Internal API
            return app.IsApi(requestedUrl);
        }
    }
}