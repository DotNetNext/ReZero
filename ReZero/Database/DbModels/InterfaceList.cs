using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Database.DbModels
{
    public class InterfaceList : DbReZeroBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public HttpRequestMethod HttpMethod { get; set; }
        public List<InterfaceParameter> Parameters { get; set; }
    }
}
