using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI
{
    /// <summary>
    /// OrdeBy
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo OrderBySelectBefore(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (IsMergeTable(dataModel))
            {
                return queryObject;
            }
            List<OrderByModel> orderByModels = new List<OrderByModel>();
            if (dataModel.OrderByFixedParemters != null)
            {
                foreach (var item in dataModel.OrderByFixedParemters)
                {
                    orderByModels.Add(new OrderByModel()
                    {
                        FieldName = GetFieldName(queryObject, new DataModelDynamicOrderParemter()
                        {
                            FieldName = item.FieldName,
                            OrderByType = item.OrderByType,
                            TableIndex = item.TableIndex
                        }),
                        OrderByType = item.OrderByType
                    });
                }
            }
            if (dataModel.OrderDynamicParemters != null)
            {
                var columns = App.Db.EntityMaintenance.GetEntityInfo(queryObject.EntityType).Columns;
                foreach (var item in dataModel.OrderDynamicParemters)
                {
                    var isAny = columns.Any(it => it.PropertyName?.ToLower() == item.FieldName?.ToLower() || it.DbColumnName?.ToLower() == item.FieldName?.ToLower());
                    if (isAny)
                    {
                        orderByModels.Add(new OrderByModel()
                        {
                            FieldName = GetFieldName(queryObject, item),
                            OrderByType = item.OrderByType
                        });
                    }
                    else if (dataModel?.SelectParameters?.Where(it => it.AsName?.ToLower() == item.FieldName?.ToLower()).Any() == true)
                    {
                        if (dataModel.MergeOrderByFixedParemters == null)
                        {
                            dataModel.MergeOrderByFixedParemters = new List<DataModelOrderParemter>();
                        } 
                    }
                    else
                    {
                        throw new Exception(TextHandler.GetCommonText("排序字段 " + item.FieldName + "不存在实体", "OrderBy " + item.FieldName + " is not exist"));
                    }
                }
            }
            queryObject = queryObject.OrderBy(orderByModels);
            return queryObject;
        }
        private QueryMethodInfo OrderBySelectAfter(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            if (!IsMergeTable(dataModel))
            {
                return queryObject;
            }
            List<OrderByModel> orderByModels = new List<OrderByModel>();
            if (dataModel.OrderByFixedParemters != null)
            {
                foreach (var item in dataModel.OrderByFixedParemters)
                {
                    orderByModels.Add(new OrderByModel()
                    {
                        FieldName = GetFieldName(queryObject, new DataModelDynamicOrderParemter()
                        {
                            FieldName = item.FieldName,
                            OrderByType = item.OrderByType,
                            TableIndex = item.TableIndex
                        }),
                        OrderByType = item.OrderByType
                    });
                }
            }
            if (dataModel.OrderDynamicParemters != null)
            {
                var columns = App.Db.EntityMaintenance.GetEntityInfo(queryObject.EntityType).Columns;
                foreach (var item in dataModel.OrderDynamicParemters)
                {
                    var isAny = columns.Any(it => it.PropertyName?.ToLower() == item.FieldName?.ToLower() || it.DbColumnName?.ToLower() == item.FieldName?.ToLower());
                    if (isAny)
                    {
                        orderByModels.Add(new OrderByModel()
                        {
                            FieldName = GetFieldName(queryObject, item),
                            OrderByType = item.OrderByType
                        });
                    }
                    else if (dataModel?.SelectParameters?.Where(it => it.AsName?.ToLower() == item.FieldName?.ToLower()).Any() == true)
                    {
                        if (dataModel.MergeOrderByFixedParemters == null)
                        {
                            dataModel.MergeOrderByFixedParemters = new List<DataModelOrderParemter>();
                        }
                    }
                    else
                    {
                        throw new Exception(TextHandler.GetCommonText("排序字段 " + item.FieldName + "不存在实体", "OrderBy " + item.FieldName + " is not exist"));
                    }
                }
            }
            queryObject = queryObject.OrderBy(orderByModels);
            return queryObject;
        }
        private string GetFieldName(QueryMethodInfo queryObject, DataModelDynamicOrderParemter item)
        {
            var name = _sqlSugarClient!.EntityMaintenance.GetDbColumnName(item.FieldName, queryObject.EntityType);
            if (this.resultType != null)
            {
                return item.FieldName;
            }
            else
            {
                return PubConst.Orm_TableDefaultPreName + item.TableIndex + "." + name;
            }
        }
    }
}
