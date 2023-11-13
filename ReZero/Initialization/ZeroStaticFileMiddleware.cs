using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ReZero.Ui;

namespace ReZero
{
    public class ZeroStaticFileMiddleware
    {
        private readonly RequestDelegate _next;

        // Constants for ReZero paths and file locations
        internal static string ReZeroDirName = "rezero";
        internal static string RezeroPathPrefix = $"/{ReZeroDirName}/";
        internal static string RezeroRootPath = $"/{ReZeroDirName}";
        internal static string DefaultIndexPath = "index.html";
        internal static string WwwRootPath = "wwwroot";
        internal static string DefaultUiFolderName = "default_ui";
        private static string UiFolderPath { get;  set; } = $"{ReZeroDirName}/{DefaultUiFolderName}";
         

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
                if (FileExistsAndIsNotHtml(filePath))
                {
                    await CopyToFile(context, filePath);
                    return;
                }
                else if (FileExistsHtml(filePath))
                {
                    await CopyToHtml(context, filePath);
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

        private static bool FileExistsHtml(string filePath)
        {
            return File.Exists(filePath) && filePath.Contains(".html");
        }

        private static bool FileExistsAndIsNotHtml(string filePath)
        {
            return File.Exists(filePath) && !filePath.Contains(".html");
        }

        // Copy the file content to the response, if the file is not a master page
        private static async Task CopyToHtml(HttpContext context, string filePath)
        {
            // Read the file content
            string fileContent;
            using (var reader = new StreamReader(filePath))
            {
                fileContent = await reader.ReadToEndAsync();
            }
            // Check if the file is a master page
            IUiManager defaultUiManager = UIFactory.uiManager;
            if (defaultUiManager.IsMasterPage(fileContent))
            {
                // If the file is a master page, get the HTML and send it to the client
                fileContent = await defaultUiManager.GetHtmlAsync(fileContent, filePath);
            }
            // Send the file content to the client
            await context.Response.WriteAsync(fileContent);
        }

        private static async Task CopyToFile(HttpContext context, string filePath)
        {
            // Read the file content and send it to the client
            using (var fileStream = File.OpenRead(filePath))
            {
                await fileStream.CopyToAsync(context.Response.Body);
            }
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
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), WwwRootPath, UiFolderPath, relativePath);
            return Path.GetFullPath(fullPath);
        }
    }
}