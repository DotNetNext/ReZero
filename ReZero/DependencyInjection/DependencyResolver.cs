using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.DependencyInjection
{
    public class DependencyResolver
    {
        public static ServiceProvider? Provider { get => ServiceLocator.Services!.BuildServiceProvider(); }

        public static T GetService<T>()
        {
            return Provider!.GetService<T>();
        } 
        public static T GetRequiredService<T>() where T:class
        {
            return Provider?.GetRequiredService<T>();
        }
        public static T GetNewService<T>() where T : class
        {
            using var scope = Provider?.CreateScope();
            return scope?.ServiceProvider?.GetService<T>();
        }
        public static T GetNewRequiredService<T>() where T : class
        {
            using var scope = Provider?.CreateScope();
            return scope?.ServiceProvider?.GetRequiredService<T>();
        } 
    }
}
