using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;
namespace ReZero 
{
    public class Authorization: DbBase
    { 
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
