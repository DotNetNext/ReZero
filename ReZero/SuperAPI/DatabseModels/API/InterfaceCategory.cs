using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class ZeroInterfaceCategory : DbBase
    {
        public string? Name { get; set; }
        [SugarColumn(IsNullable =true)]
        public long? ParentId{get;set;}
        [SugarColumn(IsNullable =true)]
        public string? Description { get; set; }
        public string? Url { get; set; } = "#";
        [SugarColumn(IsIgnore = true,ExtendedAttribute =PubConst.TreeChild)]
        public List<ZeroInterfaceCategory>? SubInterfaceCategories { get; set; }
        [SugarColumn(IsNullable =true)]
        public string? Icon { get;  set; }
    }
}
