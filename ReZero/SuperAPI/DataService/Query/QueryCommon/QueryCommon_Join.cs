using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Helper
    /// </summary>
    public partial class QueryCommon : IDataService
    { 
        private QueryMethodInfo Join(DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsAnyJoin(dataModel)) return queryObject;
            int index = 0;
            foreach (var item in dataModel.JoinParameters??new List<DataModelJoinParameters>())
            {
                index++;
                var JoinType = EntityGeneratorManager.GetTypeAsync(item.JoinTableId).GetAwaiter().GetResult();
                queryObject = queryObject.AddJoinInfo(JoinType,"t"+ index,"",item.JoinType);
            }
            return queryObject;
        }

        private static bool IsAnyJoin(DataModel dataModel)
        {
            return dataModel.JoinParameters?.Any() == true;
        }
    }
}
