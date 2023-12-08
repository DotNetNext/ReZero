using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
namespace ReZero.SuperAPI 
{
    public partial class QueryCommon : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            try
            {
                var db = App.Db;
                RefAsync<int> count = 0;
                var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
                var queryObject = db.QueryableByObject(type);
                queryObject = Where(dataModel, queryObject);
                queryObject = OrderBy(dataModel, queryObject);
                object? result = null;
                if (IsDefault(dataModel))
                {
                    result = await DefaultQuery(queryObject, result);
                }
                else
                {
                    result = await PageQuery(dataModel, count, type, queryObject, result);

                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

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
