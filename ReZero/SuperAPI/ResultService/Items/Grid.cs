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
            var dataModelOutPut = ((DataModelOutPut?)result.OutPutData);
            return new ResultPageGrid
            {
                Data = data,
                Columns = dataModelOutPut!.Columns!.Select(it => new ResultGridColumn
                {
                    PropertyName=it.PropertyName,
                    ColumnDescription=it.Description
                }),
                Page = 
                new ResultPage
                {
                    PageNumber=dataModelOutPut.Page!.PageNumber,
                    PageSize=dataModelOutPut.Page.PageSize,
                    TotalCount= dataModelOutPut.Page.TotalCount!.Value,
                    TotalPage=dataModelOutPut.Page.TotalPage
                }
   
            };
        }
    } 
}
