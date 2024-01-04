using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// OrdeBy
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo GroupName(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<GroupByModel> orderByModels = new List<GroupByModel>();
            if (dataModel.OrderParemters != null)
            {
                foreach (var item in dataModel.OrderParemters)
                {
                    orderByModels.Add(new GroupByModel()
                    {
                        FieldName = GetFieldName(queryObject, item) 
                    });
                }
            }
            queryObject = queryObject.GroupBy(orderByModels);
            return queryObject;
        } 
    }
}
