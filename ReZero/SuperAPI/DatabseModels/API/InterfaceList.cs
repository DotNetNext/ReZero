using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroInterfaceList : DbBase
    {
        public string? Url { get; set; }
        public string? Name { get; set; } 
        public long InterfaceCategoryId { get; set; }
        [SugarColumn(IsJson =true,IsNullable =true, ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public ResultModel? CustomResultModel{ get; set; } 
        [SugarColumn(IsNullable = true)]
        public string? Description { get; set; }
        public string GroupName { get; set; } = "默认分组"; 
        public string? HttpMethod { get; set; }
        [SugarColumn(IsJson = true,ColumnDataType = StaticConfig.CodeFirst_BigString)]
        public DataModel? DataModel { get; set; }
    }
}
