using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// Middleware class for handling Zero Dynamic API and Internal API requests.
    /// </summary>
    public class SuperAPIMiddleware
    {
        private readonly IApplicationBuilder _applicationBuilder;

        /// <summary>
        /// Constructor for ZeroApiMiddleware class.
        /// </summary>
        /// <param name="application">Instance of IApplicationBuilder.</param>
        public SuperAPIMiddleware(IApplicationBuilder application)
        {
            _applicationBuilder = application ?? throw new ArgumentNullException(nameof(application));
        }

        /// <summary>
        /// Middleware entry point to handle incoming requests.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        /// <param name="next">Delegate representing the next middleware in the pipeline.</param>
        public async Task InvokeAsync(HttpContext context, Func<Task> next)
        {
            // Get the requested URL path from the context
            var requestedUrl = context.Request.Path;
  
            // Check if the requested URL corresponds to Internal API
            if (IsInternalApi(requestedUrl))
            {  
                // Handle the request using Internal API logic
                await InternalApi(context);
                 
            }
            // Check if the requested URL corresponds to Dynamic API
            else if(IsDynamicApi(requestedUrl))
            { 
                // Handle the request using Dynamic API logic
                await DynamicApi(context);
                 
            }
            // If the requested URL doesn't match any specific API, pass the request to the next middleware
            else
            {
                if (IsShowNativeApiDocument(requestedUrl))
                {
                    context.Response.Redirect("/rezero/dynamic_interface.html?InterfaceCategoryId=200100");
                }
                else
                {
                    await next();
                }
            }
        }

        //private async Task<bool> AuthorizationHtmlAsync(HttpContext context)
        //{
        //    if (SuperAPIModule._apiOptions?.InterfaceOptions?.Jwt?.Enable != true) 
        //    {
        //        return true;
        //    }
        //    var url = context.Request.Path.ToString().ToLower();
        //    if (url.EndsWith(".html") == true && url != PubConst.Jwt_PageUrl)
        //    {
        //        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
        //        if (authHeader != null && authHeader.StartsWith("Bearer "))
        //        {
        //            var token = authHeader.Split(' ')[1];
        //            try
        //            {
        //                // 进行JWT令牌验证，例如使用Microsoft.AspNetCore.Authentication.JwtBearer包提供的验证器
        //                var authResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
        //                if (authResult.Succeeded)
        //                {
        //                    return true;
        //                }
        //                else
        //                {
        //                    // 用户未通过身份验证，可能需要进行一些处理，例如返回未经授权的错误
        //                    context.Response.StatusCode = 401;
        //                    context.Response.Redirect(PubConst.Jwt_PageUrl);
        //                    return false;
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                // JWT验证失败
        //                context.Response.StatusCode = 401;
        //                context.Response.Redirect(PubConst.Jwt_PageUrl);
        //                return false;
        //            }
        //        }
        //        else
        //        {
        //            // Authorization标头缺失或格式不正确
        //            context.Response.StatusCode = 401;
        //            context.Response.Redirect(PubConst.Jwt_PageUrl);
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        private static bool IsShowNativeApiDocument(PathString requestedUrl)
        {
            return requestedUrl.ToString().TrimStart('/').TrimEnd('/').ToLower() == "rezero" && SuperAPIModule._apiOptions?.UiOptions?.ShowNativeApiDocument != true;
        }

        /// <summary>
        /// Handles requests for Dynamic API.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        private async Task DynamicApi(HttpContext context)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL corresponds to Dynamic API.
        /// </summary>
        /// <param name="requestedUrl">Requested URL path.</param>
        /// <returns>True if the URL corresponds to Dynamic API, otherwise false.</returns>
        private bool IsDynamicApi(PathString requestedUrl)
        {
            // Get the IDynamicApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<IDynamicApi>();

            // Determine if the requested URL matches Dynamic API
            return app.IsApi(requestedUrl);
        }

        /// <summary>
        /// Handles requests for Internal API.
        /// </summary>
        /// <param name="context">HttpContext for the current request.</param>
        private async Task InternalApi(HttpContext context)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Invoke the WriteAsync method to process and respond to the request
            await app.WriteAsync(context);
        }

        /// <summary>
        /// Checks if the requested URL corresponds to Internal API.
        /// </summary>
        /// <param name="requestedUrl">Requested URL path.</param>
        /// <returns>True if the URL corresponds to Internal API, otherwise false.</returns>
        private bool IsInternalApi(PathString requestedUrl)
        {
            // Get the InternalApi service instance from the application's service provider
            var app = App.ServiceProvider!.GetService<InternalApi>();

            // Determine if the requested URL matches Internal API
            return app.IsApi(requestedUrl);
        }
    }
}