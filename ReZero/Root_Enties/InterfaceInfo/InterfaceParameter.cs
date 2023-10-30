using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class InterfaceParameter : DbReZeroBase
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
        public string ValueType { get; set; }
    }
}
