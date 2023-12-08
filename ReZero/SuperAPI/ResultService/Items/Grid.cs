using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI.Items
{
    public class Grid : IResultService
    {
        public object GetResult(object data, ResultModel result)
        {
            return data;
            //开发中
            return new 
            { 
                
                Data=data,
                Columns= ((DataModelOutPut)result.OutPutData).Entity.Columns.Select(it=>new { 
                   it.PropertyName,
                   it.ColumnDescription 
                }) 

            };
        }
    }
}
