using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ReZero.SuperAPI;
using System;

namespace ReZero
{
    
    public static partial class ReZeroServiceCollectionExtensions
    {
    
        public static IServiceCollection AddReZeroServices(this IServiceCollection services, ReZeroOptions options)
        {
            SuperAPIModule.Init(services, options); 

            return services;
        }
         
    }
}