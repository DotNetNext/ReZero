using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class PostRequestHandler : IRequestMethodHandler
    {
        private HttpContext context;

        public PostRequestHandler(HttpContext context)
        {
            this.context = context;
        }


        public HandleResult HandleRequest()
        {
            throw new NotImplementedException();
        }
    }

}
