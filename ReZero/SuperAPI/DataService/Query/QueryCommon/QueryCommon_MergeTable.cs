using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// MergeTable
    /// </summary>
    public partial class QueryCommon : IDataService
    {  
        private QueryMethodInfo MergeTable(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsMergeTable(dataModel))
            {
                return queryObject;
            }
            queryObject = queryObject.MergeTable();
            queryObject=MergeTableWhere(type,dataModel,queryObject);
            queryObject = OrderBySelectAfter(type, dataModel, queryObject);
            queryObject =MergeTableOrderBy(type, dataModel, queryObject);
            return queryObject;
        }

        private QueryMethodInfo MergeTableOrderBy(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsMergeOrderBy(dataModel) || resultType == null)
            {
                return queryObject;
            }
            var old = dataModel.OrderByFixedParemters;
            var oldType = queryObject.EntityType;
            dataModel.OrderByFixedParemters = dataModel.MergeOrderByFixedParemters;
            queryObject.EntityType = resultType;
            OrderBySelectAfter(resultType, dataModel, queryObject);
            dataModel.OrderByFixedParemters = old;
            queryObject.EntityType = oldType;
            return queryObject;
        }

        private QueryMethodInfo MergeTableWhere(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (dataModel?.MergeDefaultParameters?.Any()!=true|| resultType==null)
            {
                return queryObject;
            } 
            var oldType = queryObject.EntityType; 
            foreach (var item in dataModel.DefaultParameters!)
            {
                item.IsMergeWhere = false;
            }
            queryObject.EntityType = resultType; 
            Where(this.resultType, dataModel, queryObject); 
            queryObject.EntityType = oldType;
            return queryObject;
        }

        private static bool IsMergeTable(DataModel dataModel)
        {
            return IsMergeOrderBy(dataModel) || IsMergeTableWhere(dataModel);
        }

        private static bool IsMergeTableWhere(DataModel dataModel)
        {
            return dataModel.MergeOrderByFixedParemters?.Any() == true;
        }

        private static bool IsMergeOrderBy(DataModel dataModel)
        {
            return dataModel.MergeDefaultParameters?.Any() == true;
        }
    }
}
