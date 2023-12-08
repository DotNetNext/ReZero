using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Helper
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private static QueryMethodInfo Where(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.WhereParameters != null)
            {
                foreach (var item in dataModel.WhereParameters.Where(it => string.IsNullOrEmpty(it.MergeForName)).Where(it => it.Value + "" != ""))
                {
                    item.Name = App.Db.EntityMaintenance.GetDbColumnName(item.Name, queryObject.EntityType);
                    if (item.Value != null)
                    {
                        if (item.ValueType == typeof(bool).Name)
                        {
                            item.Value = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        }
                    }
                    var forNames = dataModel.WhereParameters.Where(it => it.MergeForName?.ToLower() == item.Name.ToLower()).ToList();
                    if (forNames.Any())
                    {
                        ConvetConditionalModelForNames(conditionalModels, item, forNames);
                    }
                    else
                    {
                        ConvetConditionalModelDefault(conditionalModels, item);
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

        private static void ConvetConditionalModelForNames(List<IConditionalModel> conditionalModels, WhereParameter item, List<WhereParameter> forNames)
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

        private static void ConvetConditionalModelDefault(List<IConditionalModel> conditionalModels, WhereParameter? item)
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
