using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ReZero.SuperAPI
{
    public class DefaultParameter
    {
        public string? Name { get; set; }
        public string? FieldName { get; set; }  
        public object? Value { get; set; }
        public bool ValueIsReadOnly { get; set; }
        public string? MergeForName { get; set; }
        public string? Description { get; set; }
        public string? ValueType { get; set; }  
        public FieldOperatorType? FieldOperator { get; set; }
        public string? FieldOperatorString { get { return FieldOperator?.ToString(); } }
        public UpdateParemeter? UpdateParemeter { get; set; }
        public InsertParameter? InsertParameter { get; set; }
        public QueryParameter? QueryParameter { get; set; } 
        public DeleteParameter? DeleteParameter { get; set; } 
        public ParameterValidate? ParameterValidate { get; set; }
        public string?  DefaultValue { get; set; }
    }
    public class ParameterValidate
    {
        public bool IsRequired { get; set; }
    }
    public class UpdateParemeter 
    {
       
    } 
    public class InsertParameter
    {
        public bool IsUserName { get; set; }
    }
    public class QueryParameter
    {
    }
    public class DeleteParameter
    {
    }
}
