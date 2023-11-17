using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class ZeroEntityInfo : DbBase
    { 
        public string?  ClassName { get; set; } 
        public string?  DbTableName { get; set; }
        public string ? Description { get; set; }
        [Navigate(NavigateType.OneToMany,nameof(ZeroEntityColumnInfo.TableId))]
        public List<ZeroEntityColumnInfo>? ZeroEntityColumnInfos { get; set; }
    } 
}
