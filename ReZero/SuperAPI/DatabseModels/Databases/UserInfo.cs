using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroUserInfo : DbBase
    { 
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool IsMasterAdmin { get; set; }
        [SqlSugar.SugarColumn(IsNullable =true)]
        public string? Avatar { get; set; }
    }
}
