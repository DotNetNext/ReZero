using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace ReZero 
{
    public class Public_Authorization : DbBase
    { 
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
