using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public interface IRequestMethodHandler
    {
        HandleResult HandleRequest();
    }
}
