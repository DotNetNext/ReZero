using Microsoft.AspNetCore.Http;
using System;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Helper class for handling API requests and determining appropriate request method handlers.
    /// </summary>
    internal class DynamicApiHelper
    {
        /// <summary>
        /// Determines if the provided string represents a valid HTTP request method.
        /// </summary>
        /// <param name="requestMethodString">The string representing the HTTP request method.</param>
        /// <param name="requestMethod">The parsed HttpRequestMethod enum value.</param>
        /// <returns>True if the string represents a valid HTTP request method; otherwise, false.</returns>
        public bool IsHttpMethod(string requestMethodString, out HttpRequestMethod requestMethod)
        {
            // Try to parse the request method string into HttpRequestMethod enum.
            return Enum.TryParse(requestMethodString, ignoreCase: true, out requestMethod);
        }

        /// <summary>
        /// Gets the appropriate request method handler based on the provided HTTP request method.
        /// </summary>
        /// <param name="method">The parsed HttpRequestMethod enum representing the HTTP request method.</param>
        /// <param name="context">The HttpContext associated with the request.</param>
        /// <returns>An instance of the appropriate request method handler.</returns>
        public IRequestMethodHandler GetHandler(HttpRequestMethod method, HttpContext context)
        {
            // Determine the request method and return the corresponding handler.
            switch (method)
            {
                case HttpRequestMethod.GET:
                    return new GetRequestHandler(context);
                case HttpRequestMethod.POST:
                    return new PostRequestHandler(context);
                case HttpRequestMethod.PUT:
                    return new PutRequestHandler(context);
                case HttpRequestMethod.DELETE:
                    return new DeleteRequestHandler(context);
                case HttpRequestMethod.PATCH:
                    return new PatchRequestHandler(context);
                default:
                    // Throw an exception if the request method is not supported.
                    throw new NotSupportedException("Unsupported HTTP request method");
            }
        }
    }
}
