using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.DependencyInjection;
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
            ServiceLocator.Services = services;
            SuperAPIModule.Init(services, options);
            AddDependencyInjection(options, options.SuperApiOptions);
            DependencyInjectionModule.Init(services, options);
            return services;
        }


        public static IServiceCollection AddReZeroServices(this IServiceCollection services, Action<SuperAPIOptions> superAPIOptions)
        {
            var options = new ReZeroOptions();
            ServiceLocator.Services = services; 
            superAPIOptions(options.SuperApiOptions);
            AddDependencyInjection(options, options.SuperApiOptions);
            DependencyInjectionModule.Init(services, options);
            return services.AddReZeroServices(options);
        }
        internal static void AddDependencyInjection(ReZeroOptions options, SuperAPIOptions superAPIOptions)
        {
            if (options.DependencyInjectionOptions?.Assembly == null)
            {
                if (options.DependencyInjectionOptions == null)
                {
                    options.DependencyInjectionOptions = new DependencyInjection.DependencyInjectionOptions();
                }
                options.DependencyInjectionOptions!.Assembly = superAPIOptions?.DependencyInjectionOptions?.Assembly;
            }
        }
    }
}