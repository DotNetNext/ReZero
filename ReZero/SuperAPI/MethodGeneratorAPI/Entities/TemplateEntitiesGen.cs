using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// 生成实体结构
    /// </summary>
    public class TemplateEntitiesGen
    {  
        /// <summary>
        /// 类名
        /// </summary>
        public string? ClassName { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string? TableName { get; set; }
        /// <summary>
        /// 备注 
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 列集合
        /// </summary>
        public List<TemplatePropertyGen>? PropertyGens { get; set; }

    } 
}
