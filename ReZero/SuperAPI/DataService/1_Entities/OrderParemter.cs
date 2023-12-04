using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class OrderParemter 
    {
        public string? FieldName { get; set; }
        public OrderByType OrderByType { get; set; }
    }
}
