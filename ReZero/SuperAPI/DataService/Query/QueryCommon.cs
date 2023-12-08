using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
using System.Text.RegularExpressions;
namespace ReZero.SuperAPI
{
    internal class QueryCommon : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            try
            {
                var db = App.Db;
                RefAsync<int> count = 0;
                var type = await EntityGeneratorManager.GetTypeAsync(dataModel.TableId);
                var queryObject = db.QueryableByObject(type);
                queryObject = Where(dataModel, queryObject);
                queryObject = OrderBy(dataModel, queryObject);
                object? result = null;
                if (dataModel.CommonPage == null)
                {
                    result = await queryObject.ToListAsync();
                }
                else
                {
                    result = await queryObject.ToPageListAsync(dataModel!.CommonPage!.PageNumber, dataModel.CommonPage.PageSize, count);
                    dataModel.CommonPage.TotalCount = count.Value;
                    dataModel.OutPutData = new DataModelOutPut
                    {
                        Entity = db.EntityMaintenance.GetEntityInfo(type),
                        Page = new DataModelPageParameter()
                        {
                            TotalCount = count.Value,
                            PageNumber = dataModel.CommonPage.PageNumber,
                            PageSize = dataModel.CommonPage.PageSize,
                            TotalPage = (int)Math.Ceiling((double)count.Value / dataModel.CommonPage.PageSize)
                        }
                    };
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private QueryMethodInfo OrderBy(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<OrderByModel> orderByModels = new List<OrderByModel>();
            if (dataModel.OrderParemters != null)
            {
                foreach (var item in dataModel.OrderParemters)
                {
                    orderByModels.Add(new OrderByModel()
                    {
                        FieldName = App.Db.EntityMaintenance.GetDbColumnName(item.FieldName, queryObject.EntityType),
                        OrderByType = item.OrderByType
                    });
                }
            }
            queryObject = queryObject.OrderBy(orderByModels);
            return queryObject;
        }

        private static QueryMethodInfo Where(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.WhereParameters != null)
            {
                foreach (var item in dataModel.WhereParameters.Where(it => string.IsNullOrEmpty(it.MergeForName)).Where(it => (it.Value + "") != ""))
                {
                    item.Name = App.Db.EntityMaintenance.GetDbColumnName(item.Name, queryObject.EntityType);
                    if (item.Value != null)
                    {
                        if (item.ValueType == typeof(Boolean).Name)
                        {
                            item.Value = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        }
                    }
                    var forNames = dataModel.WhereParameters.Where(it => it.MergeForName?.ToLower() == item.Name.ToLower()).ToList();
                    if (forNames.Any())
                    {
                        ForNames(conditionalModels, item, forNames);
                    }
                    else
                    {
                        Default(conditionalModels, item);
                    }
                }
            }
            queryObject = queryObject.Where(conditionalModels);
            foreach (var item in funcModels)
            {
                queryObject = queryObject.Where(item);
            }
            return queryObject;
        }

        private static void ForNames(List<IConditionalModel> conditionalModels, WhereParameter item, List<WhereParameter> forNames)
        {
            var colItem = new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" };
            var conditionalCollections = new ConditionalCollections()
            {
                ConditionalList = new List<KeyValuePair<WhereType, ConditionalModel>>()
             {
                 new KeyValuePair<WhereType, ConditionalModel>(WhereType.And,colItem)
             }
            };
            foreach (var it in forNames)
            {
                var colItemNext = new ConditionalModel() { FieldName = it.Name, ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" };
                conditionalCollections.ConditionalList.Add(new KeyValuePair<WhereType, ConditionalModel>(WhereType.Or, colItemNext));
            }
            conditionalModels.Add(conditionalCollections);
        }

        private static void Default(List<IConditionalModel> conditionalModels, WhereParameter? item)
        {
            switch (item?.FieldOperator)
            {
                case FieldOperatorType.Equal:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.Equal, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.NoEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.NoEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.GreaterThan:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.GreaterThan, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.GreaterThanOrEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.GreaterThanOrEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LessThan:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.LessThan, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LessThanOrEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.LessThanOrEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.Like:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.In:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.In, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.NotIn:
                    conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.NotIn, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                default:
                    break;
            }
        }
    }


}
