using SqlSugar;
using System;

namespace ReZero
{
    public class DbBase:IDeleted
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public int SortId { get; set; }
        public DateTime CreateTime { get; set; }
        public string? Creator { get; set; }
        public string? CreatorId { get; set; } 
        [SugarColumn(IsNullable = true)]
        public DateTime UpdateTime { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Modifier { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? ModifierId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? LanguageKey { get; set; } 
        public bool IsDeleted { get; set; }
    }

}