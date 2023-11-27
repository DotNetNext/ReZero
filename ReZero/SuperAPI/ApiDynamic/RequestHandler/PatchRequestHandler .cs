using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class PatchRequestHandler : IRequestMethodHandler
    {
        private HttpContext context;

        public PatchRequestHandler(HttpContext context)
        {
            this.context = context;
        }

        public HandleResult HandleRequest()
        {
            throw new NotImplementedException();
        }
    }
}
