using ReZero.API.RequestHandler;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.API
{
    internal class ApiHelper
    {
        public  bool IsHttpMethod(string requestMethodString, out HttpRequestMethod requestMethod)
        {
            return Enum.TryParse<HttpRequestMethod>(requestMethodString, ignoreCase: true, out requestMethod);
        }
        public IRequestMethodHandler GetHandler(HttpRequestMethod method)
        {
            switch (method)
            {
                case HttpRequestMethod.GET:
                    return new GetRequestHandler();
                case HttpRequestMethod.POST:
                    return new PostRequestHandler();
                case HttpRequestMethod.PUT:
                    return new PutRequestHandler();
                case HttpRequestMethod.DELETE:
                    return new DeleteRequestHandler();
                case HttpRequestMethod.PATCH:
                    return new PatchRequestHandler();
                default:
                    throw new NotSupportedException("Unsupported HTTP request method");
            }
        }
    }
}
