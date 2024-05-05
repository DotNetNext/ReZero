using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ApiAttribute : Attribute
    {
        internal long InterfaceCategoryId { get; set; }
        public string? GroupName { get; set; }
        public string? Description { get; set; }
        public ApiAttribute(long interfaceCategoryId)
        {
            InterfaceCategoryId = interfaceCategoryId; 
        } 
    }
}
