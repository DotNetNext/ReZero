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

            }
            else if (IsAnyJoin(dataModel))
            {
                var columns = _sqlSugarClient!.EntityMaintenance.GetEntityInfo(type).Columns.Where(it => !it.IsIgnore)
                      .Select(it => GetEntityColumns(it)).ToList();
                var selectString = String.Join(",", columns);
                queryObject = queryObject.Select(selectString);
            }
            return queryObject;
        }

        private object GetEntityColumns(EntityColumnInfo it)
        {
          return _sqlBuilder!.GetTranslationColumnName(PubConst.TableDefaultMasterTableShortName) +"."+ _sqlBuilder!.GetTranslationColumnName(it.DbColumnName) + " AS " + _sqlBuilder!.GetTranslationColumnName(it.PropertyName);
        }
    }
}
