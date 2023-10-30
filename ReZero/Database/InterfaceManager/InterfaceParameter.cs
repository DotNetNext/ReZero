using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Database.InterfaceManager
{
    public class InterfaceParameter
    {
        public string? Name { get; set; }
        public object? Value { get; set; }
        public string? ValueType { get; set; }
    }
}
