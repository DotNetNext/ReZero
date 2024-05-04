using Microsoft.AspNetCore.Http;
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
        public static IHttpContextAccessor? httpContextAccessor = null;
        public static T GetService<T>()
        {
            return Provider!.GetService<T>();
        }
        public static T GetHttpContextService<T>()
        {
            if (httpContextAccessor == null)
            {
                if (Provider!.GetService<IHttpContextAccessor>()?.HttpContext == null)
                {
                    throw new Exception("Requires builder.Services.AddHttpContextAccessor()");
                }
                httpContextAccessor = Provider!.GetService<IHttpContextAccessor>();
            }
            return httpContextAccessor!.HttpContext!.RequestServices!.GetService<T>();
        }
        public static T GetHttpContextRequiredService<T>()
        {
            if (httpContextAccessor == null)
            {
                if (Provider!.GetService<IHttpContextAccessor>()?.HttpContext == null)
                {
                    throw new Exception("Requires builder.Services.AddHttpContextAccessor()");
                }
                httpContextAccessor = Provider!.GetService<IHttpContextAccessor>();
            }
            return httpContextAccessor!.HttpContext!.RequestServices!.GetRequiredService<T>();
        }
        public static T GetRequiredService<T>() where T : class
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
