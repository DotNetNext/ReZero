using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class Zero_InterfaceCategory : DbBase
    {
        public string? Name { get; set; }
        public long ParentId{get;set;}
        public string? Description { get; set; }
    }
}
