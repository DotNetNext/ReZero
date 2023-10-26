using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ReZero 
{
    public class Api : IApi
    {
        public bool IsApi(string url)
        {
            return false;
        }

        public async Task WriteAsync(HttpContext context)
        {
            await context.Response.WriteAsync("");
        }
    }
}
