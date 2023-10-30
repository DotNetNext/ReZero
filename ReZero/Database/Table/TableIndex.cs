using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ReZero 
{
    public class TableIndex
    {
        public IndexType IndexType { get; set; }
        public string? Name { get; set; } 
        public List<TableColumnInfo>? TableColumnInfos { get; set; }
    }
}
