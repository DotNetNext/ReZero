using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// Select
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo Select(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (IsAnySelect(dataModel))
            {
                queryObject = GetSelectByParameters(type,dataModel, queryObject);
            }
            else if (IsAnyJoin(dataModel))
            {
                queryObject = GetDefaultSelect(type, queryObject);
            }
            return queryObject;
        }

        private static QueryMethodInfo GetSelectByParameters(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in dataModel.SelectParameters ?? new List<DataModelSelectParameters>())
            {
                
            }
            queryObject = queryObject.Select(sb.ToString());
            return queryObject;
        }

        private QueryMethodInfo GetDefaultSelect(Type type, QueryMethodInfo queryObject)
        {
            string selectString = GetMasterSelectAll(type);
            queryObject = queryObject.Select(selectString);
            return queryObject;
        }

        private string GetMasterSelectAll(Type type)
        {
            var columns = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(type).Columns.Where(it => !it.IsIgnore)
                  .Select(it => GetEntityColumns(it)).ToList();
            var selectString = String.Join(",", columns);
            return selectString;
        }

        private object GetEntityColumns(EntityColumnInfo it)
        {
          return _sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultMasterTableShortName) +"."+ _sqlBuilder!.GetTranslationColumnName(it.DbColumnName) + " AS " + _sqlBuilder!.GetTranslationColumnName(it.PropertyName);
        }
    }
}
