using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public static class MyServiceCollectionExtensions
    {
        public static IServiceCollection ReZero(this IServiceCollection services)
        {
            services.AddTransient<IApi, Api>();
            services.AddTransient<IStartupFilter,
                      RequestSetOptionsStartupFilter>();

            return services;
        }
    }
}
