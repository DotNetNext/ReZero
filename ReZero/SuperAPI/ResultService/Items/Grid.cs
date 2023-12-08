using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;
namespace ReZero.SuperAPI.Items
{
    public class Grid : IResultService
    {
        public object GetResult(object data, ResultModel result)
        {
            var dataModelOutPut = ((DataModelOutPut?)result.OutPutData);
            IEnumerable<ResultGridColumn> columns = GetGridColumn(dataModelOutPut);
            ResultPage page = GetPage(dataModelOutPut);
            if (data is IEnumerable<object> dataList)
            {
                data = TransformData(dataList, columns);
            }
            return new ResultPageGrid
            {
                Data = data,
                Columns = columns,
                Page = page 
            };
        }

        private static ResultPage GetPage(DataModelOutPut? dataModelOutPut)
        {
            return new ResultPage
            {
                PageNumber = dataModelOutPut!.Page!.PageNumber,
                PageSize = dataModelOutPut.Page.PageSize,
                TotalCount = dataModelOutPut.Page.TotalCount!.Value,
                TotalPage = dataModelOutPut.Page.TotalPage
            };
        }

        private static IEnumerable<ResultGridColumn> GetGridColumn(DataModelOutPut? dataModelOutPut)
        {
            return dataModelOutPut!.Columns!.Select(it => new ResultGridColumn
            {
                PropertyName = it.PropertyName,
                ColumnDescription = it.Description
            });
        }
        private static IEnumerable<object> TransformData(IEnumerable<object> dataList, IEnumerable<ResultGridColumn> columns)
        {
            var newData = new List<object>();

            foreach (var item in dataList)
            {
                var newItem = new System.Dynamic.ExpandoObject() as IDictionary<string, Object>;

                foreach (var column in columns)
                {
                    var propertyValue = GetPropertyValue(item, column.PropertyName!);
                    newItem[column.PropertyName!] = propertyValue;
                }

                newData.Add(newItem);
            }

            return newData;
        }
        private static object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo?.GetValue(obj);
        }
    } 
}
