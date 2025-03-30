using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroJwtTokenManagement : DbBase
    {
        /// <summary>
        /// 连接用户名称，标识使用 JWT 的用户
        /// </summary>
        [SugarColumn(Length = 200)]
        public string? Username { get; set; }

        /// <summary>
        /// 描述，用于说明该 JWT 授权的用途或其他相关信息
        /// </summary>
        [SugarColumn(Length =1000)]
        public string? Description { get; set; }

        /// <summary>
        /// 使用期限（授权有效期），定义 JWT 授权的最长可用时间
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// JWT Token，存储已生成的 JWT 令牌
        /// </summary>
        [SugarColumn(Length = 800)]
        public string? Token { get; set; }
    }
}
