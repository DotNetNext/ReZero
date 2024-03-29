﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    /// <summary>
    /// OrdeBy
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private QueryMethodInfo OrderBy(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<OrderByModel> orderByModels = new List<OrderByModel>();
            if (dataModel.OrderByFixedParemters  != null)
            {
                foreach (var item in dataModel.OrderByFixedParemters )
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
                foreach (var item in dataModel.OrderDynamicParemters)
                {
                    orderByModels.Add(new OrderByModel()
                    {
                        FieldName = GetFieldName(queryObject, item),
                        OrderByType = item.OrderByType
                    });
                }
            }
            queryObject = queryObject.OrderBy(orderByModels);
            return queryObject;
        }

        private  string GetFieldName(QueryMethodInfo queryObject, DataModelDynamicOrderParemter item)
        {
            var name = _sqlSugarClient!.EntityMaintenance.GetDbColumnName(item.FieldName, queryObject.EntityType);
            return PubConst.Orm_TableDefaultPreName + item.TableIndex + "." + name;
        }
    }
}
