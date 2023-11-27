using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace ReZero.SuperAPI
{
    public class ZeroAuthorization : DbBase
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
