using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class JoinParameter
    {
        public string? LeftPropertyName { get;   set; }
        public FieldOperatorType FieldOperator { get;   set; }
        public string? RightPropertypeName { get;   set; }
        public int LeftIndex { get;   set; }
        public int RightIndex { get;   set; }
    }
}
