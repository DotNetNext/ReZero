using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DynamicMethodAttribute : Attribute
    {
        private string url { get; set; }
        private long InterfaceCategoryId { get; set; }
        private string GroupName { get; set; }
        public DynamicMethodAttribute(string url,long interfaceCategoryId, string groupName) 
        {
            this.url = url;
            this.InterfaceCategoryId = interfaceCategoryId;
            this.GroupName = groupName; 
        }
        public DynamicMethodAttribute(string url, long interfaceCategoryId)
        {
            this.url = url;
            this.InterfaceCategoryId = interfaceCategoryId;
            this.GroupName =string.Empty;
        }
        public DynamicMethodAttribute(string url)
        {
            this.url = url;
            this.InterfaceCategoryId = 0;
            this.GroupName = string.Empty;
        }
        public DynamicMethodAttribute()
        {
            this.url = string.Empty;
            this.InterfaceCategoryId = 0;
            this.GroupName = string.Empty;
        }
    }
}
