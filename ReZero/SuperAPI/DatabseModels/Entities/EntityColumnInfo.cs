using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroEntityColumnInfo : DbBase
    {
        public long TableId { get; set; }
        public string? DbCoumnName { get; set; }
        public string? PropertyName { get; set; } 
        public int Length { get; set; }
        public int? DecimalDigits { get; set; }

        public string? Description { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsPrimarykey { get; set; }
        public bool IsArray { get; set; }
        public bool IsJson { get; set; }
        public bool IsNullable { get; set; }
        public int Scale { get; set; }
        public bool? IsUnsigned { get; set; }
        public NativeTypes PropertyType { get; set; }
        public string? DataType { get; set; }
        [SugarColumn(IsNullable=true)]
        public object? ExtendedAttribute { get;  set; }
    }
}
