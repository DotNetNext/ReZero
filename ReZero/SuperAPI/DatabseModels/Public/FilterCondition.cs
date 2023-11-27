using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace ReZero.SuperAPI
{
    public class ZeroPublicFilterCondition : DbBase
    {
        /// <summary>
        /// 可以是用户ID或者角色ID，根据上下文传递相应的值
        /// </summary>
        public long PrincipalId { get; set; }
        /// <summary>
        /// PrincipalId 类型
        /// </summary>
        public PrincipalType PrincipalType { get; set; }
        public string? TableName { get; set; }
        public long? TableId { get; set; } 
        [SugarColumn(IsJson =true)]
        public JsonArray? FilterExpression { get; set; } 
    }
}
