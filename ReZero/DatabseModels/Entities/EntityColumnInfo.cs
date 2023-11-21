using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroEntityColumnInfo
    {
        public long TableId { get; set; }
        public string? DbCoumnName { get; set; }
        public string? PropertyName { get; set; }
        public NativeTypes NativeTypes { get; set; }
        public string? DbColumnDataType { get; set; }
        public int Length { get; set; }
        public int ? DecimalDigits { get; set; }

        public string? Description { get; set; } 
        public bool IsIdentity { get; internal set; }
        public bool IsPrimarykey { get;   set; }
        public bool IsArray { get; internal set; }
        public bool IsJson { get; internal set; }
        public bool IsNullable { get; internal set; }
        public int Scale { get; internal set; }
        public bool? IsUnsigned { get; internal set; }
        public NativeTypes PropertyType { get;   set; }
        public string? DataType { get; internal set; } 
    }
}
