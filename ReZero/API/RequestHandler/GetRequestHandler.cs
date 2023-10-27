using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.API.RequestHandler
{
    public class GetRequestHandler : IRequestMethodHandler
    {
        public string HandleRequest()
        {
            return "Handling GET request";
        }
    }
}
