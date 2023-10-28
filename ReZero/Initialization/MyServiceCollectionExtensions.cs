using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.Api_ReZero.Interface;
using System;

namespace ReZero
{
    /// <summary>
    /// Extension methods for configuring ReZero services in IServiceCollection.
    /// </summary>
    public static class MyServiceCollectionExtensions
    {
        /// <summary>
        /// Adds ReZero services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which ReZero services are added.</param>
        /// <param name="options">Optional ReZero options.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection ReZero(this IServiceCollection services, ReZeroOptions? options = null)
        {
            // If options are not provided, create a new instance of ReZeroOptions.
            options = options ?? new ReZeroOptions();

            // Add services to the IServiceCollection.
            services.AddTransient<IDynamicApi, DynamicApi>();
            services.AddTransient<IReZeroApi, ReZeroApi>();
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();

            // Create an instance of ORM with the specified connection configuration and add it as a transient service.
            services.AddTransient<DatabaseReZeroContext>(it => new DatabaseReZeroContext(options.ConnectionConfig));

            // Return the updated IServiceCollection.
            return services;
        }
    }
}