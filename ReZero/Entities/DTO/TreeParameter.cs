using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class TreeParameter
    {
        public string?  CodePropertyName { get; set; }
        public object? RootValue { get; set; }
        public string?  ParentCodePropertyName { get; set; }
        public string? ChildPropertyName { get; set; }
    }
}
