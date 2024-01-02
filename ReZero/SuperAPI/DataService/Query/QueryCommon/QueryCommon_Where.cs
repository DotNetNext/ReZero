using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ReZero.SuperAPI 
{
    /// <summary>
    /// Where
    /// </summary>
    public partial class QueryCommon : IDataService
    {
        private static QueryMethodInfo Where(Type type, DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.DefaultParameters != null)
            {
                foreach (var item in dataModel.DefaultParameters.Where(it => string.IsNullOrEmpty(it.MergeForName)).Where(it => it.Value + "" != ""))
                {
                    item.Name = App.Db.EntityMaintenance.GetDbColumnName(item.Name, queryObject.EntityType);
                    if (item.Value != null)
                    {
                        if (item.ValueType == typeof(bool).Name)
                        {
                            if (item.Value?.ToString().EqualsCase( "true" )==true|| item.Value?.ToString().EqualsCase("false")==true)
                            {
                                item.Value = Convert.ToBoolean(item.Value);
                            }
                            else
                            {
                                item.Value = Convert.ToBoolean(Convert.ToInt32(item.Value));
                            }
                        }
                    }
                    var forNames = dataModel.DefaultParameters.Where(it => it.MergeForName?.ToLower() == item.Name.ToLower()).ToList();
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

        private static void ConvetConditionalModelForNames(List<IConditionalModel> conditionalModels, DataModelDefaultParameter item, List<DataModelDefaultParameter> forNames)
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
         
        private static void ConvetConditionalModelDefault(List<IConditionalModel> conditionalModels, DataModelDefaultParameter? item)
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
                default:
                    break;
            }
        }


        private static string GetFieldName(DataModelDefaultParameter item)
        {
            return PubConst.TableDefaultPreName + item.TableIndex + "." + item.Name;
        }
    }
}
