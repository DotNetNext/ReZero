using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.Configuration
{
    public class ReZeroUiBasicdatabase
    {
        public SqlSugar.DbType? DbType { get; set; }
        public string? ConnectionString { get; set; }
    }
}
