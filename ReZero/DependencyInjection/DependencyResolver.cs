﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ReZero.SuperAPI;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ReZero.DependencyInjection
{
    public class DependencyResolver
    {
        public static ServiceProvider Provider { get => ServiceLocator.Services!.BuildServiceProvider(); }
        public static IHttpContextAccessor? httpContextAccessor = null;
        public static ILogger<SuperAPIMiddleware>? logger = null;
        public static T GetService<T>() where T : class
        {
            return Provider!.GetService<T>();
        }

        public static ILogger<SuperAPIMiddleware> GetLogger() 
        {
            if (logger == null) 
            {
                logger = ReZero.DependencyInjection.DependencyResolver.GetService<ILogger<SuperAPIMiddleware>>();
            }
            return logger;
        }
        public static ClaimsPrincipal GetClaims()
        {
            if (httpContextAccessor == null)
            {
                if (Provider!.GetService<IHttpContextAccessor>()?.HttpContext == null)
                {
                    throw new Exception("Requires builder.Services.AddHttpContextAccessor()");
                }
                httpContextAccessor = Provider!.GetService<IHttpContextAccessor>();
            }
            HttpContext httpContext = httpContextAccessor!.HttpContext;
            var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                var handler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = handler.ReadJwtToken(token);
                    var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
                    return claimsPrincipal;
                }
                catch (Exception)
                {
                    // Handle token parsing error
                    return null;
                }
            }

            return null;
        }
        public static T GetHttpContextService<T>() where T : class
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
        public static T GetHttpContextRequiredService<T>() where T : class
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
        public static string GetLoggedInUser()
        {
            var claimsPrincipal = GetClaims();
            if (claimsPrincipal == null)
            {
                return null;
            }

            var usernameClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == "unique_name");
            if (usernameClaim == null)
            {
                return  null;
            }

            return usernameClaim.Value; ;
        }
    }
}
