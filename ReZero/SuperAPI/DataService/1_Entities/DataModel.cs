using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero.SuperAPI
{
    public class DataModel
    {
        public ActionType ActionType { get; set; }
        public object? Data { get; set; } 
        public long TableId { get; set; }
        public List<OrderParemter>? OrderParemters { get; set; }
        public List<WhereParameter>? WhereParameters { get; set; } 
        public DataModelPageParameter? CommonPage { get; set; }
        public DataModelTreeParameter? TreeParameter { get; set; } 
        public DataModelJoinParameters? DataModelJoinParameters { get; set; }
        [Navigate(NavigateType.OneToMany,nameof(TableId))]

        public ZeroEntityInfo? MasterEntityInfo { get; set; }
        public object? OutPutData { get; set; }
        public List<DataColumnParameter>? Columns { get;   set; }
    }
}
