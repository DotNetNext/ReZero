using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI.ApiDynamic.Entities
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DynamicApiAttribute : Attribute
    {
        private long InterfaceCategoryId { get; set; }
        private string GroupName { get; set; }
        public DynamicApiAttribute(long interfaceCategoryId, string groupName)
        {
            this.InterfaceCategoryId = interfaceCategoryId;
            this.GroupName = groupName;
        }
        public DynamicApiAttribute(long interfaceCategoryId)
        {
            this.InterfaceCategoryId = interfaceCategoryId;
            this.GroupName = string.Empty;
        }
    }
}
