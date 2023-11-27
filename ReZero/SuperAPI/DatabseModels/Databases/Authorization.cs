using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace ReZero 
{
    public class ZeroAuthorization : DbBase
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
