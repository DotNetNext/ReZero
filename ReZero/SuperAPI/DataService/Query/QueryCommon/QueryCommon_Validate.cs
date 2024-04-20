using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Validate
    /// </summary>
    public partial class QueryCommon : IDataService
    { 
        private static bool IsDefaultToList(DataModel dataModel)
        {
            return dataModel.CommonPage == null;
        } 
        private static bool IsAnyJoin(DataModel dataModel)
        {
            return dataModel.JoinParameters?.Any() == true;
        } 
        private static bool IsAnySelect(DataModel dataModel)
        {
            return dataModel.SelectParameters?.Any() == true;
        } 
        private static bool IsSelectMasterAll(DataModelSelectParameters item)
        {
            return item.IsTableAll && item.TableIndex == 0;
        } 
        private static bool IsSelectJoinName(DataModelSelectParameters item)
        {
            return item.IsTableAll == false && item.TableIndex > 0;
        }
        private static bool IsSelectSubqueryName(DataModelSelectParameters item)
        {
            return item.Name ==PubConst.Orm_SubqueryKey;
        }
    }
}
