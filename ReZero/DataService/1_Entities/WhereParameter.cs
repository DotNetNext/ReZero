using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class WhereParameter
    {  
        public long TableId { get; set; }  
        public string? ProperyName { get; set; }
        public string? FieldName { get; set; }
        public string? FieldValue { get; set; } 
        public string? Description { get; set; } 
        public FieldOperatorType? FieldOperator { get; set; } 
    }
}
