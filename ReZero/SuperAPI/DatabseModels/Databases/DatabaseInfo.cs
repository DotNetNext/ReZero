using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroDatabaseInfo : DbBase
    {
        public string? Name { get; set; } 
        public string? Connection { get; set; }
        public DbType DbType { get; set; }
    }
}
