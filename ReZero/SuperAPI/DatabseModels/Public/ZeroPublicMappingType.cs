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
        public  NativeType NativeTypes { get; set; }
        [SqlSugar.SugarColumn(IsNullable = true)]
        public  NativeType NativeTypesDescription { get; set; }
        public string? DataType { get; set; }

    }
}
