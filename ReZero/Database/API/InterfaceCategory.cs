using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroInterfaceCategory : DbBase
    {
        public string? Name { get; set; }
        [SugarColumn(IsNullable =true)]
        public long? ParentId{get;set;}
        [SugarColumn(IsNullable =true)]
        public string? Description { get; set; }
        [SugarColumn(IsIgnore =true)]
        public List<ZeroInterfaceCategory>? SubInterfaceCategories { get; set; }
        public string? Url { get;  set; }
    }
}
