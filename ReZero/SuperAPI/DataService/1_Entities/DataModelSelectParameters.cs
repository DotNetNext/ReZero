using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DataModelSelectParameters
    {
        public int TableIndex { get; set; }
        public string?  Name { get; set; }
        public OrderByType OrderByType { get; set; }
    } 
}
