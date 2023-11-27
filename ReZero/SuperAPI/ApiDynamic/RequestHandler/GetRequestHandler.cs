using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class GetRequestHandler : IRequestMethodHandler
    {
        private HttpContext context;

        public GetRequestHandler(HttpContext context)
        {
            this.context = context;
        }

        public HandleResult HandleRequest()
        {
            throw new NotImplementedException();
        }
    }
}