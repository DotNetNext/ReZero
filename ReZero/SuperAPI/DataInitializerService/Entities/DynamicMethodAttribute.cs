using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiMethodAttribute : Attribute
    {
        private string url { get; set; }
        private long InterfaceCategoryId { get; set; }
        private string GroupName { get; set; }
        public ApiMethodAttribute(string url, long interfaceCategoryId, string groupName)
        {
            this.url = url;
            InterfaceCategoryId = interfaceCategoryId;
            GroupName = groupName;
        }
        public DynamicMethodAttribute(string url, long interfaceCategoryId)
        {
            this.url = url;
            InterfaceCategoryId = interfaceCategoryId;
            GroupName = string.Empty;
        }
        public DynamicMethodAttribute(string url)
        {
            this.url = url;
            InterfaceCategoryId = 0;
            GroupName = string.Empty;
        }
        public DynamicMethodAttribute()
        {
            url = string.Empty;
            InterfaceCategoryId = 0;
            GroupName = string.Empty;
        }
    }
}
