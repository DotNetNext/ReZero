using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ResultColumnModel
    {
        public string? PropertyName { get; set; } 
        public Type? ConvertType { get; set; }
        public Type? ConvertType2 { get; set; }
        public   ResultColumnType ResultColumnType { get; set; }
    }
}
