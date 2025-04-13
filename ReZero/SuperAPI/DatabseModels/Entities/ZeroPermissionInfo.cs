using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroPermissionInfo : DbBase
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string? Name { get; set; } 
    }

    public class ZeroPermissionMapping : DbBase
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? UserName { get; set; }

        /// <summary>
        /// 接口ID
        /// </summary>
        public long? InterfaceId { get; set; }

        /// <summary>
        /// 权限信息ID
        /// </summary>
        public long? PermissionInfoId { get; set; }
    }
}
