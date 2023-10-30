﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ColumnFilterRule : DbBase
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
        public string? AllowedColumns { get; set; } 
    }
}