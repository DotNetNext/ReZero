using SqlSugar;
using System;

namespace ReZero
{
    public class DbBase:IDeleted
    {
        [SugarColumn(IsPrimaryKey = true)]
        public long Id { get; set; }
        public int SortId { get; set; }
        [SugarColumn(IsOnlyIgnoreUpdate =true,InsertServerTime =true)]
        public DateTime CreateTime { get; set; }
        public string? Creator { get; set; } = "";
        public long CreatorId { get; set; } 
        [SugarColumn(UpdateServerTime =true,IsNullable =true,IsOnlyIgnoreInsert =true)]
        public DateTime UpdateTime { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? Modifier { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? ModifierId { get; set; }
        [SugarColumn(IsNullable = true)]
        public string? LanguageKey { get; set; } 
        public bool IsDeleted { get; set; }
        [SugarColumn(IsNullable =true)]
        public string? EasyDescription { get; set; }
        public bool IsInitialized { get; set; }
    }

}