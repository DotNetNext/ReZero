using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class PatchRequestHandler : IRequestMethodHandler
    {
        public string HandleRequest()
        {
            return "Handling PATCH request";
        }
    }
}
