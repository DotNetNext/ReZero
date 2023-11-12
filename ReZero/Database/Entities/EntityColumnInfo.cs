using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroEntityColumnInfo
    {
        public long TableId { get; set; }
        public string? Name { get; set; }
        public NativeTypes NativeTypes { get; set; }
        public string? DbColumnDataType { get; set; }
        public int Length { get; set; }
        public int ? DecimalDigits { get; set; }

        public string? Description { get; set; }
    }
}
