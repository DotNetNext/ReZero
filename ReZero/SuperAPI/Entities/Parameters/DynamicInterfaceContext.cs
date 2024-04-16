using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class InterfaceContext
    {
        public InterfaceType InterfaceType { get; internal set; }
        public HttpContext? Context { get; internal set; }
        public DataModel? DataModel { get; internal set; }
        public ZeroInterfaceList? InterfaceInfo { get; internal set; }
    }
    public enum InterfaceType 
    {
        DynamicApi,
        SystemApi,
    }
}
