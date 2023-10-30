using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class TableInfo:DbBase
    {
        public string? ClassName { get; set; }
        public string? TableName { get; set; }   
        public string? Description { get; set; }  
    }
}
