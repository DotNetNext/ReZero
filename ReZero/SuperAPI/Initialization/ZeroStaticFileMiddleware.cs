using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http; 

namespace ReZero.SuperAPI
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
        private static string UiFolderPath { get; set; } = $"{ReZeroDirName}/{DefaultUiFolderName}";

        public ZeroStaticFileMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invokes the middleware to handle the request.
        /// </summary>
        /// <param name="context">The HttpContext for the request.</param>
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

                var filePathByCurrentDirectory = GetFilePathByCurrentDirectory(path);
                var filePathByBaseDirectory = GetFilePathByBaseDirectory(path);
       
                if (FileExistsAndIsNotHtml(filePathByCurrentDirectory))
                {
                    await CopyToFile(context, filePathByCurrentDirectory);
                    return;
                }
                else if (FileExistsHtml(filePathByCurrentDirectory))
                {
                    await CopyToHtml(context, filePathByCurrentDirectory);
                    return;
                }
                else if (FileExistsAndIsNotHtml(filePathByBaseDirectory))
                {
                    await CopyToFile(context, filePathByBaseDirectory);
                    return;
                }
                else if (FileExistsHtml(filePathByBaseDirectory))
                {
                    await CopyToHtml(context, filePathByBaseDirectory);
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

        /// <summary>
        /// Checks if the requested file exists and is not an HTML file.
        /// </summary>
        /// <param name="filePath">The path of the file to check.</param>
        /// <returns>True if the file exists and is not an HTML file, false otherwise.</returns>
        private static bool FileExistsAndIsNotHtml(string filePath)
        {
            return File.Exists(filePath) && !filePath.Contains(".html");
        }

        /// <summary>
        /// Checks if the requested file exists and is an HTML file.
        /// </summary>
        /// <param name="filePath">The path of the file to check.</param>
        /// <returns>True if the file exists and is an HTML file, false otherwise.</returns>
        private static bool FileExistsHtml(string filePath)
        {
            return File.Exists(filePath) && filePath.Contains(".html");
        }

        /// <summary>
        /// Copies the content of the requested file to the response stream.
        /// </summary>
        /// <param name="context">The HttpContext for the request.</param>
        /// <param name="filePath">The path of the file to copy.</param>
        private static async Task CopyToFile(HttpContext context, string filePath)
        {
            // Read the file content and send it to the client
            using (var fileStream = File.OpenRead(filePath))
            {
                await fileStream.CopyToAsync(context.Response.Body);
            }
        }

        /// <summary>
        /// Copies the content of the requested HTML file to the response stream.
        /// </summary>
        /// <param name="context">The HttpContext for the request.</param>
        /// <param name="filePath">The path of the HTML file to copy.</param>
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
                fileContent = await defaultUiManager.GetHtmlAsync(fileContent, filePath, context);
            }
            // Send the file content to the client
            await context.Response.WriteAsync(fileContent);
        }

        /// <summary>
        /// Checks if the requested URL is for a ReZero static file.
        /// </summary>
        /// <param name="path">The path of the requested URL.</param>
        /// <returns>True if the requested URL is for a ReZero static file, false otherwise.</returns>
        private static bool IsRezeroFileUrl(string path)
        {
            return path.StartsWith(RezeroPathPrefix) && path.Contains(".");
        }

        /// <summary>
        /// Checks if the requested URL is the root URL of ReZero.
        /// </summary>
        /// <param name="path">The path of the requested URL.</param>
        /// <returns>True if the requested URL is the root URL of ReZero, false otherwise.</returns>
        private static bool IsRezeroRootUrl(string path)
        {
            return path.TrimEnd('/') == RezeroRootPath;
        }

    
        private static string GetFilePathByCurrentDirectory(string path)
        {
            var relativePath = path.Replace(RezeroPathPrefix, string.Empty);
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), WwwRootPath, UiFolderPath, relativePath);
            return Path.GetFullPath(fullPath);
        }
        private static string GetFilePathByBaseDirectory(string path)
        {
            var relativePath = path.Replace(RezeroPathPrefix, string.Empty);
            var fullPath = Path.Combine(AppContext.BaseDirectory, WwwRootPath, UiFolderPath, relativePath);
            return Path.GetFullPath(fullPath);
        }
    }
}