using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero 
{
    public class DbTableInfo:SqlSugar.DbTableInfo
    {
        public List<DbColumnInfo>? ColumnInfos { get; set; }
    }
}
