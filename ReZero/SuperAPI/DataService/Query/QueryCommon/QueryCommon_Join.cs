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
        private QueryMethodInfo Join(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsAnyJoin(dataModel)) return queryObject;
            int index = 0;
            var joinInfoList = dataModel.JoinParameters ?? new List<DataModelJoinParameters>();
            foreach (var item in joinInfoList)
            {
                index++;
                var shortName = GetShortName(index);
                var JoinType = EntityGeneratorManager.GetTypeAsync(item.JoinTableId).GetAwaiter().GetResult();
                var onSql = GetJoinOnSql(type,item.OnList, shortName, joinInfoList);
                queryObject = queryObject.AddJoinInfo(JoinType, shortName, onSql, item.JoinType);
            }
            return queryObject;
        }

        private string GetJoinOnSql(Type type, List<JoinParameter>? onList, string shortName, List<DataModelJoinParameters> joinInfoList)
        {
            string onSql = string.Empty;
            return onSql;
        } 

        private static string GetShortName(int index)
        {
            return "t" + index;
        }

    }
}
