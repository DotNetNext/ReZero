using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    internal class ZeroPublicMappingType:DbBase
    { 
        public SqlSugar.DbType DbType { get; set; }
        [SqlSugar.SugarColumn(IsNullable =true)]
        public string? DbTypeDescription { get; set; }
        public ReZero.NativeTypes NativeTypes { get; set; }
        [SqlSugar.SugarColumn(IsNullable = true)]
        public ReZero.NativeTypes NativeTypesDescription { get; set; }
        public string? DataType { get; set; }

    }
}
