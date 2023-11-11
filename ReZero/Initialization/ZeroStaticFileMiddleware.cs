using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ReZero
{
    public class ZeroStaticFileMiddleware
    {
        private readonly RequestDelegate _next;

        // Constants for ReZero paths and file locations
        private const string RezeroPathPrefix = "/rezero/";
        private const string RezeroRootPath = "/rezero";
        private const string DefaultIndexPath = "index.html";
        private const string WwwRootPath = "wwwroot";
        private const string RezeroStaticPath = "rezero/default_ui";

        public ZeroStaticFileMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Get the lowercase path of the request
            var path = (context.Request.Path.Value ?? string.Empty).ToLower();

            // Check if the request is for the root URL of ReZero
            if (IsRezeroRootUrl(path))
            {
                // Redirect to the default index.html if it is the root URL
                context.Response.Redirect($"{RezeroPathPrefix}{DefaultIndexPath}");
                return;
            }
            // Check if the request is for a ReZero static file
            else if (IsRezeroFileUrl(path))
            {
                // Get the full path of the requested file
                var filePath = GetFilePath(path);

                // Check if the file exists
                if (File.Exists(filePath))
                {
                    // Read the file content and send it to the client
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        await fileStream.CopyToAsync(context.Response.Body);
                    }

                    return;
                }
                else
                {
                    // If the file does not exist, return a 404 Not Found status
                    context.Response.StatusCode = 404;
                    return;
                }
            }

            // If the request doesn't match ReZero paths, call the next middleware
            await _next(context);
        }

        // Check if the requested URL is for a ReZero static file
        private static bool IsRezeroFileUrl(string path)
        {
            return path.StartsWith(RezeroPathPrefix) && path.Contains(".");
        }

        // Check if the requested URL is the root URL of ReZero
        private static bool IsRezeroRootUrl(string path)
        {
            return path.TrimEnd('/') == RezeroRootPath;
        }

        // Get the full path of the requested ReZero static file
        private static string GetFilePath(string path)
        {
            var relativePath = path.Replace(RezeroPathPrefix, string.Empty);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), WwwRootPath, RezeroStaticPath, relativePath);
            return Path.GetFullPath(fullPath);
        }
    }
}