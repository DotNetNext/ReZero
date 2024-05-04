using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
namespace ReZero.DependencyInjection
{ 
    public class DependencInitialization
    {
        public IServiceCollection Init(IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes().Where(type => !type.IsAbstract && !type.IsInterface);

            foreach (var type in types)
            {
                var interfaces = type.GetInterfaces();
                foreach (var @interface in interfaces)
                {
                    if (@interface == typeof(ITransientContract))
                    {
                        services.AddTransient(@interface, type);
                    }
                    else if (@interface == typeof(IScopeContract))
                    {
                        services.AddScoped(@interface, type);
                    }
                    else if (@interface == typeof(ISingletonContract))
                    {
                        services.AddSingleton(@interface, type);
                    }
                }
                var properties = type.GetProperties().Where(prop => prop.GetCustomAttribute<PropertyInjectionAttribute>() != null);
                foreach (var property in properties)
                {
                    var serviceType = property.PropertyType;
                    var service = services.BuildServiceProvider().GetService(serviceType);
                    if (service != null)
                    {
                        property.SetValue(null, service);
                    }
                }
            }
            return services;
        }
    }
}