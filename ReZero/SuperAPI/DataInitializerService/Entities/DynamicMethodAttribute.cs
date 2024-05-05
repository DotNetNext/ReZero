using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace ReZero.SuperAPI
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiMethodAttribute : Attribute
    {
        public string? Url { get; set; }
        public string? GroupName { get; set; }
        public HttpType HttpMethod { get; set; } 
        internal string? Description { get; set; }
        public ApiMethodAttribute(string description)
        {
            this.Description = description;
        }
    }
    public enum HttpType
    { 
        Post=0,
        Get=1,
        Put=2,
        Delete=3
    }
}
