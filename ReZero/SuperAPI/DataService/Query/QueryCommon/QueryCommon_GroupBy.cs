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
        private QueryMethodInfo GroupBy(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<GroupByModel> groupByModels = new List<GroupByModel>();
            if (dataModel.GroupParemters != null)
            {
                foreach (var item in dataModel.GroupParemters!)
                {
                    groupByModels.Add(new GroupByModel()
                    {
                        FieldName = GetGroupByFieldName(queryObject,item)  
                    });
                }
            }
            queryObject = queryObject.GroupBy(groupByModels);
            return queryObject;
        }
        private string GetGroupByFieldName(QueryMethodInfo queryObject, DataModelGroupParameter item)
        {
            var name = App.Db.EntityMaintenance.GetDbColumnName(item.FieldName, queryObject.EntityType);
            return PubConst.Orm_TableDefaultPreName + item.TableIndex + "." + name;
        }
    }
}
