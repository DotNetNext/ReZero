using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace ReZero 
{
    public class PublicAuthorization : DbBase
    { 
        public string? UserId { get; set; }
        public string? RoleId { get; set; }
    }
}
