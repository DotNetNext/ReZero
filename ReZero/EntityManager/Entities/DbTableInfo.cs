using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class DbTableInfo:SqlSugar.DbTableInfo
    {
        [SugarColumn(IsPrimaryKey =true)]
        public long Id { get; set; }
        public List<DbColumnInfo>? ColumnInfos { get; set; }
    }
}
