using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DataModelDynamicOrderParemter 
    {
        public string? FieldName { get; set; }
        public OrderByType OrderByType { get; set; }
        public int TableIndex { get;  set; }
    }
    public class DataModelOrderParemter
    {
        public string? FieldName { get; set; }
        public OrderByType OrderByType { get; set; }
        public int TableIndex { get; set; }
    }
}
