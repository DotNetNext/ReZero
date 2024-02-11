using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DynamicInterfaceContext
    {
        public HttpContext? Context { get; internal set; }
    }
}
