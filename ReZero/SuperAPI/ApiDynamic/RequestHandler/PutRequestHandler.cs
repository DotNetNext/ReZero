using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class PutRequestHandler : IRequestMethodHandler
    {
        private HttpContext context;

        public PutRequestHandler(HttpContext context)
        {
            this.context = context;
        }

        public HandleResult HandleRequest()
        {
            throw new NotImplementedException();
        }
    }
}
