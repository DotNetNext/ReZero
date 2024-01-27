using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.SuperAPI;
using System;

namespace ReZero
{
    
    public static partial class ReZeroServiceCollectionExtensions
    {

        /// <summary>
        /// Adds ReZero services to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <param name="options">The <see cref="ReZeroOptions"/> to configure the services.</param>
        /// <returns>The modified <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, ReZeroOptions options)
        {
            SuperAPIModule.Init(services, options); 
            return services;
        }

       
        public static IServiceCollection AddReZeroServices(this IServiceCollection services,Action<ReZeroOptions> func)
        {
            var options = new ReZeroOptions(); 
            func(options);
            return AddReZeroServices(services, options);
        }
    }
}