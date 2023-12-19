using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class DataModelJoinParameters
    {
        public JoinType JoinType { get;  set; }
        public long JoinTableId { get;   set; }
        public List<JoinParameter>? OnList { get;  set; }
    }
}
