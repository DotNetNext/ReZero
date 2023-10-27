using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class PutRequestHandler : IRequestMethodHandler
    {
        public string HandleRequest()
        {
            return "Handling PUT request";
        }
    }
}
