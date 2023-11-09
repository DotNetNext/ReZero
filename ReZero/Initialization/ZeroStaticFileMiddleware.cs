using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
namespace ReZero 
{ 

    public class ZeroStaticFileMiddleware
    {
        private readonly RequestDelegate _next;

        public ZeroStaticFileMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.ToLower()+"";

            // 检查请求路径是否匹配静态文件路径
            if (path.StartsWith("/rezero/"))
            {
                // 获取静态文件的物理路径
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "rezero", "default_ui", path.Replace("/rezero/",""));

                // 检查文件是否存在
                if (File.Exists(filePath))
                {
                    // 如果文件存在，将其发送到客户端
                    var fileContent = File.ReadAllBytes(filePath);
                    await context.Response.Body.WriteAsync(fileContent, 0, fileContent.Length);
                    return;
                }
                else
                {
                    // 如果文件不存在，返回404 Not Found
                    context.Response.StatusCode = 404;
                    return;
                }
            }

            // 如果请求不匹配静态文件路径，则调用下一个中间件
            await _next(context);
        }
    }
}
