using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class DataModelOutPut
    {
        public EntityInfo? Entity { get; set; }
        public DataModelPageParameter? Page { get;  set; }
    }
}
