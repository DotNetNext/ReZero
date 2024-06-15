using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.CompilerServices;
namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Where
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        public   QueryMethodInfo Where(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.DefaultParameters != null)
            {
                AddDynamicOrderBy(dataModel);
                dataModel.WhereRelation = dataModel.WhereRelation ?? WhereRelation.And;
                switch (dataModel.WhereRelation)
                {
                    case WhereRelation.And:
                        And(dataModel, queryObject, conditionalModels);
                        break;
                    case WhereRelation.AndAll:
                        AndAll(dataModel, queryObject, conditionalModels);
                        break;
                    case WhereRelation.Or:
                        Or(dataModel, queryObject, conditionalModels);
                        break;
                    case WhereRelation.OrAll:
                        OrAll(dataModel, queryObject, conditionalModels);
                        break;
                    case WhereRelation.Custom:
                        Custom(dataModel, queryObject, conditionalModels);
                        break;
                    case WhereRelation.CustomAll:
                        CustomAll(dataModel, queryObject, conditionalModels);
                        break;
                }
            }
            queryObject = queryObject.Where(conditionalModels,true);
            foreach (var item in funcModels)
            {
                queryObject = queryObject.Where(item);
            }
            return queryObject;
        }

        private static void AddDynamicOrderBy(DataModel dataModel)
        {
            if (IsOrderByParameters(dataModel))
            {
                if (dataModel.OrderDynamicParemters == null)
                {
                    dataModel.OrderDynamicParemters = new List<DataModelDynamicOrderParemter>();
                }
                var name = dataModel.DefaultParameters.FirstOrDefault(it => it.Name == "OrderByName").Value?.ToString();
                var sortType = Convert.ToInt32(dataModel.DefaultParameters.FirstOrDefault(it => it.Name == "OrderByType").Value);
                if (!string.IsNullOrEmpty(name))
                {
                    dataModel.OrderDynamicParemters = new List<DataModelDynamicOrderParemter>();
                    dataModel.OrderDynamicParemters.Add(new DataModelDynamicOrderParemter()
                    {
                        FieldName = name,
                        OrderByType = sortType == 0 ? OrderByType.Asc : OrderByType.Desc
                    });
                }
                dataModel.DefaultParameters!.RemoveAll(it => it.Name == "OrderByName" || it.Name == "OrderByType");
            }
        }

        private static bool IsOrderByParameters(DataModel dataModel)
        {
            return dataModel.DefaultParameters.Any(it => it.Name == "OrderByType") && dataModel.DefaultParameters.Any(it => it.Name == "OrderByName");
        }

        private   void And(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            foreach (var item in dataModel.DefaultParameters.Where(it => it.IsMergeWhere!=true&&string.IsNullOrEmpty(it.MergeForName)).Where(it => it.Value + "" != ""))
            {
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
            }
        }
        
        private   void AndAll(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            foreach (var item in dataModel.DefaultParameters.Where(it => it.IsMergeWhere != true&& string.IsNullOrEmpty(it.MergeForName)))
            {
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
            }
        }
        
        private   void Or(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            foreach (var item in dataModel.DefaultParameters.Where(it => it.IsMergeWhere != true&& string.IsNullOrEmpty(it.MergeForName)).Where(it => it.Value + "" != ""))
            {
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
            }
            var conditionalList = conditionalModels.Select(it=>new KeyValuePair<WhereType, ConditionalModel>(WhereType.Or,(ConditionalModel)it)).ToList();
            conditionalModels.Clear(); 
            conditionalModels.Add(new ConditionalCollections()
            {
                 ConditionalList= conditionalList,
            });
            if (conditionalList.Count == 0) 
            {
                conditionalModels.Clear();
                conditionalModels.Add(new ConditionalModel()
                {
                    FieldName = UtilMethods.FieldNameSql(), 
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = PubConst.Orm_SqlFalseString
                });
            }
        }
        
        private   void OrAll(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            foreach (var item in dataModel.DefaultParameters.Where(it => it.IsMergeWhere != true&& string.IsNullOrEmpty(it.MergeForName)))
            {
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
            }
            var conditionalList = conditionalModels.Select(it => new KeyValuePair<WhereType, ConditionalModel>(WhereType.Or, (ConditionalModel)it)).ToList();
            conditionalModels.Clear();
            conditionalModels.Add(new ConditionalCollections()
            {
                ConditionalList = conditionalList,
            });
            if (conditionalList.Count == 0)
            {
                conditionalModels.Clear();
                conditionalModels.Add(new ConditionalModel()
                {
                    FieldName = UtilMethods.FieldNameSql(),
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = PubConst.Orm_SqlFalseString
                });
            }
        }
        
        private   void Custom(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            var temp = dataModel.WhereRelationTemplate+string.Empty;
            List<SugarParameter> sugarParameters = new List<SugarParameter>();
            var index = 0;
            foreach (var item in dataModel.DefaultParameters!.Where(it => it.IsMergeWhere != true))
            {
                index++;
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
                var conditional=conditionalModels.Last();
                var sql = queryObject.Context.Utilities.ConditionalModelsToSql(new List<IConditionalModel>() { conditional }, index);
                if (item.ValueIsReadOnly)
                {
                    temp = temp.Replace($"{{{item.Id}}}", sql.Key);
                    sugarParameters.AddRange(sql.Value);
                }
                else if (item.Value?.Equals(string.Empty)==true) 
                {
                    temp = temp.Replace($"{{{item.Id}}}",$" 1=1 ");
                }
                else
                {
                    temp = temp.Replace($"{{{item.Id}}}", sql.Key);
                    sugarParameters.AddRange(sql.Value);
                }
            }
            queryObject.Where(temp, sugarParameters);
            conditionalModels.Clear();
        }
        
        private   void CustomAll(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels)
        {
            var temp = dataModel.WhereRelationTemplate + string.Empty;
            List<SugarParameter> sugarParameters = new List<SugarParameter>();
            var index = 0;
            foreach (var item in dataModel.DefaultParameters.Where(it=>it.IsMergeWhere!=true)!)
            {
                index++;
                ConvetConditional(dataModel, queryObject, conditionalModels, item);
                var conditional = conditionalModels.Last();
                var sql = queryObject.Context.Utilities.ConditionalModelsToSql(new List<IConditionalModel>() { conditional }, index);
                if (item.ValueIsReadOnly)
                {
                    temp = temp.Replace($"{{{item.Id}}}", sql.Key);
                    sugarParameters.AddRange(sql.Value);
                }
                else
                {
                    temp = temp.Replace($"{{{item.Id}}}", sql.Key);
                    sugarParameters.AddRange(sql.Value);
                }
            }
            queryObject.Where(temp, sugarParameters);
            conditionalModels.Clear();
        }

        private  void ConvetConditional(DataModel dataModel, QueryMethodInfo queryObject, List<IConditionalModel> conditionalModels, DataModelDefaultParameter? item)
        {
            var preoperyName = item?.Name;
            item!.Name =_sqlSugarClient!.EntityMaintenance.GetDbColumnName(item.PropertyName??item.Name, queryObject.EntityType);
            if (item.Value != null)
            {
                if (item.ValueType == typeof(bool).Name)
                {
                    if (item.Value?.ToString().EqualsCase("true") == true || item.Value?.ToString().EqualsCase("false") == true)
                    {
                        item.Value = Convert.ToBoolean(item.Value);
                    }
                    else
                    {
                        item.Value = Convert.ToBoolean(Convert.ToInt32(item.Value));
                    }
                }
            }
            if (item.ValueType == PubConst.Orm_WhereValueTypeClaimKey)
            {
                if (!dataModel.ClaimList.Any(it => it.Key?.ToLower() == item.Value?.ToString()?.ToLower()))
                {
                    throw new SqlSugarException(TextHandler.GetCommonText("授权失败，没有找到Claim Key" + item.Value, "Authorization failure ,ClaimList Not Found Key:" + item.Value));
                }
                var value = dataModel.ClaimList.FirstOrDefault(it => it.Key?.ToLower() == item.Value?.ToString()?.ToLower()).Value;
                item.Value = value;
                item.ValueType = value?.GetType()?.Name;
            }
            var forNames = dataModel.DefaultParameters.Where(it => it.MergeForName?.ToLower() == (preoperyName)?.ToLower()).ToList();
            if (forNames.Any())
            {
                foreach (var forItem in forNames)
                {
                    forItem.Name = _sqlSugarClient!.EntityMaintenance.GetDbColumnName(forItem.Name, queryObject.EntityType);
                }
                ConvetConditionalModelForNames(conditionalModels, item, forNames);
            }
            else
            {
                ConvetConditionalModelDefault(conditionalModels, item);
            }
        }

        private  void ConvetConditionalModelForNames(List<IConditionalModel> conditionalModels, DataModelDefaultParameter item, List<DataModelDefaultParameter> forNames)
        {
            var colItem = new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" };
            var conditionalCollections = new ConditionalCollections()
            {
                ConditionalList = new List<KeyValuePair<WhereType, ConditionalModel>>()
             {
                 new KeyValuePair<WhereType, ConditionalModel>(WhereType.And,colItem)
             }
            };
            foreach (var it in forNames)
            {
                var colItemNext = new ConditionalModel() { FieldName = GetFieldName(it), ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" };
                conditionalCollections.ConditionalList.Add(new KeyValuePair<WhereType, ConditionalModel>(WhereType.Or, colItemNext));
            }
            conditionalModels.Add(conditionalCollections);
        }
         
        private void ConvetConditionalModelDefault(List<IConditionalModel> conditionalModels, DataModelDefaultParameter? item)
        {
            switch (item?.FieldOperator)
            {
                case FieldOperatorType.Equal:
                    conditionalModels.Add(new ConditionalModel() { FieldName =GetFieldName(item), ConditionalType = ConditionalType.Equal, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.NoEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.NoEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.GreaterThan:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.GreaterThan, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.GreaterThanOrEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.GreaterThanOrEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LessThan:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.LessThan, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LessThanOrEqual:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.LessThanOrEqual, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.Like:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.Like, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.In:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.In, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.NotIn:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.NotIn, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LikeLeft:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.LikeLeft, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.LikeRight:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.LikeRight, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                case FieldOperatorType.NoLike:
                    conditionalModels.Add(new ConditionalModel() { FieldName = GetFieldName(item), ConditionalType = ConditionalType.NoLike, CSharpTypeName = item.ValueType, FieldValue = item.Value + "" });
                    break;
                default:
                    break;
            }
        }

        private  string GetFieldName(DataModelDefaultParameter item)
        {
            if (this.resultType != null)
            {
                return item.Name;
            }
            else
            {
                return PubConst.Orm_TableDefaultPreName + item.TableIndex + "." + item.Name;
            }
        }
    }
}
