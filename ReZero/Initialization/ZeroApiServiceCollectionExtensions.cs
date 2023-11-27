using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.SuperAPI.Initialization;
using System;

namespace ReZero
{
    /// <summary>
    /// Extension methods for configuring ReZero services in IServiceCollection.
    /// </summary>
    public static partial class ReZeroServiceCollectionExtensions
    {
    
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, ReZeroOptions options)
        {
            if (IsInitSupperApi(options))
            {
                InitSupperApi(services, options);
            }
            return services;
        }
         
    }
}