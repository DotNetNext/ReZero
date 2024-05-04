using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
namespace ReZero.DependencyInjection
{ 
    public class DependencyInjectionModule
    {
        public static void Init(IServiceCollection services, ReZeroOptions options)
        {
            if (options.DependencyInjectionOptions?.Assemblies?.Any()!=true) 
            {
                return;
            }
            var types = options.DependencyInjectionOptions.Assemblies.SelectMany(it=>it.GetTypes()).Where(type => !type.IsAbstract && !type.IsInterface);
            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                var interfacesNoRezero = type.GetInterfaces().Where(it => !it.FullName.StartsWith("ReZero."));
                foreach (var @interface in interfaces)
                {
                    if (@interface == typeof(ITransientContract))
                    {
                        services.AddTransient(type, type);
                        foreach (var item in interfacesNoRezero)
                        {
                            services.AddTransient(item, type);
                        }
                    }
                    else if (@interface == typeof(IScopeContract))
                    { 
                        services.AddScoped(type, type);
                        foreach (var item in interfacesNoRezero)
                        {
                            services.AddScoped(item, type);
                        }
                    }
                    else if (@interface == typeof(ISingletonContract))
                    { 
                        services.AddSingleton(type, type);
                        foreach (var item in interfacesNoRezero)
                        {
                            services.AddSingleton(item, type);
                        }
                    }
                } 
            } 
        }
    }
}