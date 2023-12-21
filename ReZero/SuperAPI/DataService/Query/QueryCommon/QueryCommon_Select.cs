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
                queryObject = queryObject.Select($"{PubConst.TableDefaultMasterTableShortName}.*");
            }
            return queryObject;
        }

    }
}
