using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ZeroTemplate : DbBase
    {
        public TemplateType TypeId { get; set; }
        public string? Title { get; set; }
        [SugarColumn(ColumnDataType =StaticConfig.CodeFirst_BigString)]
        public string ? TemplateContent { get; set; }
        [SugarColumn(IsNullable =true)]
        public string? TemplateContentStyle { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Url { get; set; }
    }
}
