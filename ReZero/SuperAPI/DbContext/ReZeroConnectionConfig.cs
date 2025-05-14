using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReZero.SuperAPI 
{

    public class SuperAPIConnectionConfig
    {
        public SqlSugar.DbType DbType { get; set; }
        public string? ConnectionString { get;   set; }
        public bool Oracle_VersionLe11 { get;  set; }
        public SqlSugar.DbType? DatabaseModel { get;  set; }
    }
}
