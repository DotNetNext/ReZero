using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero
{
    public class EntityMappingService
    {
        
        public Action<ZeroEntityInfo>? TableInfoConvertFunc { get; set; }

 
        public Action<ZeroEntityColumnInfo>? TableColumnInfoConvertFunc { get; set; }

  
        public ZeroEntityInfo ConvertDbToEntityInfo(DbTableInfo dbTableInfo)
        {
   
            return null;
        }
         
        public DbTableInfo ConvertEntityToDbTableInfo(ZeroEntityInfo entityInfo)
        {
            // 实现转换逻辑
            return null;
        }
    }
}
