using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text; 

namespace ReZero.SuperAPI
{
    public class DataModel
    {
        #region Core 
        public ActionType ActionType { get; set; }
        public object? Data { get; set; }
        public long TableId { get; set; }
        public MyMethodInfo? MyMethodInfo { get; set; }
        #endregion

        #region Paremters

        public List<OrderParemter>? OrderParemters { get; set; }
        public List<DefaultParameter>? DefaultParameters { get; set; }
        public DataModelPageParameter? CommonPage { get; set; }
        public DataModelTreeParameter? TreeParameter { get; set; }
        public DataModelJoinParameters? JoinParameters { get; set; }
        #endregion
  
        #region Other
        [Navigate(NavigateType.OneToMany, nameof(TableId))]
        public ZeroEntityInfo? MasterEntityInfo { get; set; }
        public object? OutPutData { get; set; }
        public List<DataColumnParameter>? Columns { get; set; } 
        #endregion
    }
}
