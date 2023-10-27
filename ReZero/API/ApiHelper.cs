using Microsoft.AspNetCore.Http;
using ReZero.API.RequestHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    internal class ApiHelper
    {
        public  bool IsHttpMethod(string requestMethodString, out HttpRequestMethod requestMethod)
        {
            return Enum.TryParse<HttpRequestMethod>(requestMethodString, ignoreCase: true, out requestMethod);
        }
        public IRequestMethodHandler GetHandler(HttpRequestMethod method, HttpContext context)
        {
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
                    throw new NotSupportedException("Unsupported HTTP request method");
            }
        }
    }
}
