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
        public bool IsRequired { get; set; }  
        public FieldOperatorType? FieldOperator { get; set; }
        public string? FieldOperatorString { get { return FieldOperator?.ToString(); } }
        public WhereParameterOnlyUpdate? WhereParameterOnlyUpdate { get; set; }
        public WhereParameterOnlyInsert? WhereParameterOnlyInsert { get; set; }
        public WhereParameterOnlyQuery? WhereParameterOnlyQuery { get; set; } 
        public WhereParameterOnlyDelete? WhereParameterOnlyDelete { get; set; } 
        public string?  DefaultValue { get; set; }
    }
    public class WhereParameterOnlyUpdate 
    {
       
    } 
    public class WhereParameterOnlyInsert
    {
        public bool IsUserName { get; set; }
    }
    public class WhereParameterOnlyQuery
    {
    }
    public class WhereParameterOnlyDelete
    {
    }
}
