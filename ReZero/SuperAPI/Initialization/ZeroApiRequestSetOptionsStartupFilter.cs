using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ReZero
{
    /// <summary>
    /// Custom startup filter to configure application services and middleware.
    /// </summary>
    public class ZeroApiRequestSetOptionsStartupFilter : IStartupFilter
    {
        /// <summary>
        /// Configures application services and middleware.
        /// </summary>
        /// <param name="next">The next middleware delegate.</param>
        /// <returns>An action to configure the application builder.</returns>
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            // Return an action to configure the application builder.
            return builder =>
            {
                // Initialize the application service provider with the application builder.
                App.ServiceProvider = new ApplicationServiceProvider(builder);

                // Create an instance of ZeroApiMiddleware and handle API requests.
                Func<HttpContext, Func<Task>, Task> func = async (context, next) =>await new ZeroApiMiddleware(builder).InvokeAsync(context, next);

                // Use the created middleware in the pipeline.
                builder.Use(func);

                builder.UseMiddleware<ZeroStaticFileMiddleware>();

                // Call the next middleware in the pipeline.
                next(builder);
            };
        }
    }
}