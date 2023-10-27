using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class DeleteRequestHandler : IRequestMethodHandler
    {
        private HttpContext context;

        public DeleteRequestHandler(HttpContext context)
        {
            this.context = context;
        }

        public HandleResult HandleRequest()
        {
            throw new NotImplementedException();
        }
    }
}
