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
        ResultModel? _result;
        public object GetResult(object data, ResultModel result)
        {
            _result = result;
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
        private  IEnumerable<object> TransformData(IEnumerable<object> dataList, IEnumerable<ResultGridColumn> columns)
        {
            var newData = new List<object>();
            ResultColumnService resultColumnService = new ResultColumnService();
            foreach (var item in dataList)
            {
                var newItem = new System.Dynamic.ExpandoObject() as IDictionary<string, Object>;

                foreach (var column in columns)
                {
                    var propertyValue = GetPropertyValue(item, column.PropertyName!);
                    if (IsConvertColumn(column))
                    {
                        var resultColumnModel = _result?.ResultColumnModels?.First(it => it.PropertyName == column.PropertyName);
                        propertyValue = resultColumnService.GetValue(propertyValue, resultColumnModel); ;
                    }
                    newItem[column.PropertyName!] = propertyValue;
                }

                newData.Add(newItem);
            }

            return newData;
        }

        private bool IsConvertColumn(ResultGridColumn column)
        {
            return _result?.ResultColumnModels?.Any(it => it.PropertyName == column.PropertyName) == true;
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo?.GetValue(obj);
        }
    } 
}
