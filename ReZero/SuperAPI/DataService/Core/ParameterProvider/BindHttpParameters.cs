using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class BindHttpParameters
    {
        internal void Bind(DataModel? dataModel, HttpContext context)
        {
            var formDatas = GetForDatams(context);
            BindPageParameters(dataModel, context, formDatas);
            BindDefaultParameters(dataModel, context, formDatas);
        }

        private void BindPageParameters(DataModel? dataModel, HttpContext context, Dictionary<string, string> formDatas)
        {
            if (dataModel?.CommonPage != null)
            {
                var data = dataModel?.DefaultParameters?.FirstOrDefault(it => it?.Name?.EqualsCase(nameof(DataModelPageParameter.PageNumber)) == true);
                if (data != null)
                {
                    data.Value = GetParameterValueFromRequest(data, context, formDatas);
                    dataModel!.CommonPage.PageNumber = Convert.ToInt32(data.Value ?? "1");
                }
            }
        }

        private void BindDefaultParameters(DataModel? dataModel, HttpContext context, Dictionary<string, string> formDatas)
        {
            if (dataModel!.DefaultParameters != null)
            {
                dataModel!.DefaultParameters = dataModel?.DefaultParameters?.Where(it => NoPageParameters(it)).ToList();
                foreach (var item in dataModel?.DefaultParameters ?? new List<DefaultParameter>())
                {
                    UpdateWhereItemValue(context, formDatas, item);
                }
            }
        }

        private void UpdateWhereItemValue(HttpContext context, Dictionary<string, string> formDatas, DefaultParameter item)
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

        private static bool NoPageParameters(DefaultParameter it)
        {
            return it.Name != nameof(DataModelPageParameter.PageNumber) &&
                        it.Name != nameof(DataModelPageParameter.PageSize);
        }

        private static bool IsUserName(DefaultParameter item)
        {
            return item?.InsertParameter?.IsUserName == true;
        }

        private static bool IsDefaultValue(DefaultParameter item)
        {
            return item.Value == null && item.DefaultValue != null;
        }

        private string GetParameterValueFromRequest(DefaultParameter parameter, HttpContext context, Dictionary<string, string> formDatas)
        {
            if (parameter.ValueIsReadOnly)
            {
                return parameter.Value + "";
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
