using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ReZero 
{
    public class Zero_TableIndex : DbBase
    {
        public IndexType IndexType { get; set; }
        public string? Name { get; set; } 
        public List<Zero_TableColumnInfo>? TableColumnInfos { get; set; }
    }
}
