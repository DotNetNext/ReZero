using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public static class MyServiceCollectionExtensions
    {
        public static IServiceCollection ReZero(this IServiceCollection services, ReZeroOptions? options=null)
        {
            options = options ?? new ReZeroOptions();


            services.AddTransient<IApi, Api>();
            services.AddTransient<IStartupFilter,RequestSetOptionsStartupFilter>();
            services.AddTransient<ORM>(it => new ORM(options.ConnectionConfig));

            return services;
        }
    }
}
