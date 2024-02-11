using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public interface ISuperApi
    {

        bool IsApi(string url);
        Task WriteAsync(HttpContext context);
    }
}
