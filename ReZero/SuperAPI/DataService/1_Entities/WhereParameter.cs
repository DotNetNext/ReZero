using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class WhereParameter
    {
        public string? Name { get; set; }
        public string? FieldName { get; set; }  
        public object? Value { get; set; }
        public bool ValueIsReadOnly { get; set; }
        public string? MergeForName { get; set; }
        public string? Description { get; set; }
        public string? ValueType { get; set; } 
        public bool IsRequired { get; set; }  
        public FieldOperatorType? FieldOperator { get; set; }
        public string? FieldOperatorString { get { return FieldOperator?.ToString(); } }
    }
}
