using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class TableColumnInfo:DbReZeroBase
    {
        public string? PropertyName { get; set; }
        public string? DbColumnName { get; set; }
        public TableColumnTypeInfo? TableColumnTypeInfo { get; set; }
        public string? Description { get; set; }
    }
}
