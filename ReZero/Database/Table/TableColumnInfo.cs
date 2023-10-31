using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class Zero_TableColumnInfo : DbBase
    {
        public string? PropertyName { get; set; }
        public string? DbColumnName { get; set; }
        public Zero_TableColumnTypeInfo? TableColumnTypeInfo { get; set; }
        public string? Description { get; set; }
    }
}
