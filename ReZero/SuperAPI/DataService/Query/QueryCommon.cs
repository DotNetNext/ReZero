using SqlSugar;
using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Data;
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
                if (dataModel.CommonPage == null)
                {
                    var result = queryObject.ToList();
                    return result;
                }
                else
                {
                    var result = queryObject.ToPageListAsync(dataModel!.CommonPage!.PageNumber, dataModel.CommonPage.PageSize, count);
                    return result;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static QueryMethodInfo Where(DataModel dataModel, QueryMethodInfo queryObject)
        {
            List<IConditionalModel> conditionalModels = new List<IConditionalModel>();
            List<IFuncModel> funcModels = new List<IFuncModel>();
            if (dataModel.WhereParameters != null)
            {
                foreach (var item in dataModel.WhereParameters.Where(it=>(it.Value+"")!=""))
                {
                    item.Name = App.Db.EntityMaintenance.GetDbColumnName(item.Name,queryObject.EntityType);
                    if (item.Value != null)
                    {
                        if (item.ValueType == typeof(Boolean).Name)
                        {
                            item.Value = Convert.ToBoolean(Convert.ToInt32(item.Value));
                        }
                    }
                    switch (item.FieldOperator) 
                    {
                        case FieldOperatorType.Equal:
                            conditionalModels.Add(new ConditionalModel() { FieldName = item.Name, ConditionalType = ConditionalType.Equal,CSharpTypeName=item.ValueType, FieldValue = item.Value+"" });
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
            queryObject = queryObject.Where(conditionalModels);
            foreach (var item in funcModels)
            {
                queryObject = queryObject.Where(item);
            }
            return queryObject;
        }
    }
}
