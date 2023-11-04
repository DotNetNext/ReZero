using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class Zero_UserInfo : DbBase
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool IsMasterAdmin { get; set; }
    }
}
