using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class PostRequestHandler : IRequestMethodHandler
    {
        public string HandleRequest()
        {
            return "Handling POST request";
        }
    }

}
