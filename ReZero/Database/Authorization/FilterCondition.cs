using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace ReZero 
{
    public class FilterCondition:DbBase
    { 
        public int CriteriaId { get; set; }
        public int UserId { get; set; }
        public string? TableName { get; set; }
        [SugarColumn(IsJson =true)]
        public JsonArray? FilterExpression { get; set; } 
    }
}
