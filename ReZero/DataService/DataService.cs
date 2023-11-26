using Microsoft.AspNetCore.Http;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Threading.Tasks;

namespace ReZero 
{
    public class DataService : IDataService
    {
        public async Task<object> ExecuteAction(DataModel dataModel)
        {
            try
            {
                var actionTypeName = GetActionTypeName(dataModel);
                var actionType = Type.GetType(actionTypeName);
                CheckActionType(dataModel, actionType);
                var actionInstance = (IDataService)Activator.CreateInstance(actionType);
                var result = await actionInstance.ExecuteAction(dataModel);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        private static string GetActionTypeName(DataModel dataModel)
        {
            return $"ReZero.{dataModel.ActionType}";
        } 

        private static void CheckActionType(DataModel dataModel, Type actionType)
        {
            if (actionType == null)
            {
                throw new ArgumentException($"Invalid ActionType: {dataModel.ActionType}");
            }
        }

        internal void BindHttpParameters(DataModel? dataModel, HttpContext context)
        {
            foreach (var item in  dataModel?.WhereParameters??new System.Collections.Generic.List<WhereParameter>())
            {
                item.Value = GetParameterValueFromRequest(item,context);
            }
        }
        private string GetParameterValueFromRequest(WhereParameter parameter,HttpContext context)
        {
            // 假设你希望获取名为 "parameterName" 的查询字符串参数
            string parameterValue = context.Request.Query[parameter.Name];

            parameter.Value = parameterValue;

            return parameterValue;
        }
    }
}