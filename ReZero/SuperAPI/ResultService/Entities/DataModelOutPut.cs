using Microsoft.Data.SqlClient.DataClassification;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DataModelOutPut
    {  
        public DataModelPageParameter? Page { get;  set; }
        public List<DataColumnParameter>? Columns { get; internal set; }
    }
}
