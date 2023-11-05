using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class DataModel
    {
        public ActionType ActionType { get; set; }
        public object? Data { get; set; } 
        public Type? MasterEntityType { get; set; }
        public CommonPage? CommonPage { get; set; }
    }
}
