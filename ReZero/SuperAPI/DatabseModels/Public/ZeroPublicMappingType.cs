using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class ZeroPublicMappingType:DbBase
    { 
        public SqlSugar.DbType DbType { get; set; }
        [SqlSugar.SugarColumn(IsNullable =true)]
        public string? DbTypeDescription { get; set; }
        public  NativeTypes NativeTypes { get; set; }
        [SqlSugar.SugarColumn(IsNullable = true)]
        public  NativeTypes NativeTypesDescription { get; set; }
        public string? DataType { get; set; }

    }
}
