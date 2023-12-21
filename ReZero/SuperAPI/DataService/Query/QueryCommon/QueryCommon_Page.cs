using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Page
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private static async Task<object?> PageQuery(DataModel dataModel, RefAsync<int> count, Type type, QueryMethodInfo queryObject, object? result)
        {
            result = await queryObject.ToPageListAsync(dataModel!.CommonPage!.PageNumber, dataModel.CommonPage.PageSize, count);
            dataModel.CommonPage.TotalCount = count.Value;
            if (dataModel.Columns?.Any() == false)
            {
                dataModel.Columns = App.Db.EntityMaintenance.GetEntityInfo(type).Columns.Select(it => new DataColumnParameter
                {
                    PropertyName = it.PropertyName,
                    Description = it.ColumnDescription
                }).ToList();
            }
            dataModel.OutPutData = new DataModelOutPut
            {
                Page = new DataModelPageParameter()
                {
                    TotalCount = count.Value,
                    PageNumber = dataModel.CommonPage.PageNumber,
                    PageSize = dataModel.CommonPage.PageSize,
                    TotalPage = (int)Math.Ceiling((double)count.Value / dataModel.CommonPage.PageSize)
                },
                Columns = dataModel.Columns
            };
            return result;
        }

        private static async Task<object?> DefaultQuery(QueryMethodInfo queryObject, object? result)
        {
            result = await queryObject.ToListAsync();
            return result;
        }

    }
}
