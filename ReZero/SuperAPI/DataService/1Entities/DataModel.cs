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
        public long DataBaseId { get; set; }
        public MyMethodInfo? MyMethodInfo { get; set; }
        public string? Sql { get; set; }
        public SqlResultType? ResultType { get; set; }
        #endregion

        #region Paremters

        public List<DataModelDynamicOrderParemter>? OrderDynamicParemters { get; set; }
        public List<DataModelOrderParemter>? OrderByFixedParemters  { get; set; }
        public List<DataModelOrderParemter>? MergeOrderByFixedParemters { get; set; }
        public List<DataModelDefaultParameter>? DefaultParameters { get; set; }
        public List<DataModelDefaultParameter>? MergeDefaultParameters { get; set; }
        public DataModelPageParameter? CommonPage { get; set; }
        public DataModelTreeParameter? TreeParameter { get; set; }
        public List<DataModelJoinParameters>? JoinParameters { get; set; }
        public List<DataModelSelectParameters>? SelectParameters { get; set; }
        public List<DataModelGroupParameter>? GroupParemters { get; set; }
        #endregion

        #region Other
        [Navigate(NavigateType.OneToMany, nameof(TableId))]
        public ZeroEntityInfo? MasterEntityInfo { get; set; }
        public object? OutPutData { get; set; }
        public long ApiId { get; set; }
        public List<DataColumnParameter>? Columns { get; set; }
        public WhereRelation? WhereRelation { get;  set; }
        public string? WhereRelationTemplate { get;  set; }
        public string? CurrentDataString { get;   set; }
        #endregion

        #region Http

        internal object? ServiceProvider { get; set; }
        internal Dictionary<string, object>? ClaimList { get; set; } = new Dictionary<string, object>();
        #endregion
    }
}
