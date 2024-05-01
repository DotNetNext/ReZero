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
            queryObject=MergeTableOrderBy(type, dataModel, queryObject);
            return queryObject;
        }

        private QueryMethodInfo MergeTableOrderBy(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsMergeOrderBy(dataModel))
            {
                return queryObject;
            }
            return queryObject;
        }

        private QueryMethodInfo MergeTableWhere(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsMergeTableWhere(dataModel))
            {
                return queryObject;
            }
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
