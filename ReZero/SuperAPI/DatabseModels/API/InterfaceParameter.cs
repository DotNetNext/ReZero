using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroInterfaceParameter
    {
        public string? Name { get; set; }
        public object? Value { get; set; }
        public bool ValueIsReadOnly { get; set; }
        public string? Description { get; set; }
        public string? ValueType { get; set; }
    }
}
