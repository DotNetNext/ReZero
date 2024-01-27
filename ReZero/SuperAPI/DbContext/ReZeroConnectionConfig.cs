using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ReZero.SuperAPI 
{

    public class ReZeroConnectionConfig
    {
        public SqlSugar.DbType DbType { get; set; }
        public string? ConnectionString { get;   set; }
    }
}
