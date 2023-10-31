using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class InterfaceList : DbBase
    { 
        public string? Url { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public HttpRequestMethod HttpMethod { get; set; }
        public List<InterfaceParameter>? Parameters { get; set; }
    }
}
