using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Helper
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo OrderBy(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<OrderByModel> orderByModels = new List<OrderByModel>();
            if (dataModel.OrderParemters != null)
            {
                foreach (var item in dataModel.OrderParemters)
                {
                    orderByModels.Add(new OrderByModel()
                    {
                        FieldName = App.Db.EntityMaintenance.GetDbColumnName(item.FieldName, queryObject.EntityType),
                        OrderByType = item.OrderByType
                    });
                }
            }
            queryObject = queryObject.OrderBy(orderByModels);
            return queryObject;
        }
    }
}
