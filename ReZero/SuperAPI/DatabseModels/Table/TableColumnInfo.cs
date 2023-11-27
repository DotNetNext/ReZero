using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroTableColumnInfo : DbBase
    {
        public string? PropertyName { get; set; }
        public string? DbColumnName { get; set; }
        public ZeroTableColumnTypeInfo? TableColumnTypeInfo { get; set; }
        public string? Description { get; set; }
    }
}
