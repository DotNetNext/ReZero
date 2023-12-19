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
            if (dataModel.JoinParameters?.Any() == true) 
            {

            }
            return queryObject;
        }
    }
}
