using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Helper
    /// </summary>
    public partial class QueryCommon : IDataService
    { 
        private static bool IsDefault(DataModel dataModel)
        {
            return dataModel.CommonPage == null;
        }
 
    }
}
