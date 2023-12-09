using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using SqlSugar;
using SqlSugar.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    public class DataService :IDataService
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
            return $"ReZero.SuperAPI.{dataModel.ActionType}";
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
            var formDatas = GetForDatams(context);
            foreach (var item in dataModel?.WhereParameters ?? new System.Collections.Generic.List<WhereParameter>())
            {

                item.Value = GetParameterValueFromRequest(item, context, formDatas);
                if (IsDefaultValue(item))
                {
                    item.Value = item.DefaultValue;
                }
                if (IsUserName(item))
                {
                    var options = SuperAPIModule._apiOptions;
                    item.Value = options?.GetCurrentUserCallback().UserName;
                }
                if (!string.IsNullOrEmpty(item?.FieldName))
                {
                    item.Name = item.FieldName;
                }
            }
        }

        private static bool IsUserName(WhereParameter item)
        {
            return item?.WhereParameterOnlyInsert?.IsUserName == true;
        }

        private static bool IsDefaultValue(WhereParameter item)
        {
            return item.Value == null && item.DefaultValue != null;
        }

        private string GetParameterValueFromRequest(WhereParameter parameter, HttpContext context, Dictionary<string, string> formDatas)
        {
            if (parameter.ValueIsReadOnly) 
            {
                return parameter.Value+"";
            }
            // 假设你希望获取名为 "parameterName" 的查询字符串参数
            string parameterValue = context.Request.Query[parameter.Name];
            if (formDatas.ContainsKey(parameter.Name ?? ""))
                parameterValue = formDatas[parameter.Name ?? ""];
            parameter.Value = parameterValue;

            return parameterValue;
        }

        private static Dictionary<string, string> GetForDatams(HttpContext context)
        {
            Dictionary<string, string> formDatas = new Dictionary<string, string>();
            if (context.Request.Body != null)
            {
                // 从请求正文中读取参数值
                using var reader = new System.IO.StreamReader(context.Request.Body);
                var body = reader.ReadToEndAsync().Result;
                if (!string.IsNullOrEmpty(body))
                {
                    formDatas = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(body) ?? new Dictionary<string, string>();

                }
            }
            return formDatas ?? new Dictionary<string, string>();
        }
    }
}