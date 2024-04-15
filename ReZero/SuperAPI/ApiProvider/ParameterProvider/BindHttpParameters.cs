using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class BindHttpParameters
    {
        internal void Bind(DataModel? dataModel, HttpContext context)
        {
            var formDatas = GetFormDatas(context);
            BindPageParameters(dataModel, context, formDatas);
            BindDefaultParameters(dataModel, context, formDatas);
            BindOrderByParameters(dataModel, context, formDatas);
            BindGroupByParameters(dataModel, context, formDatas);
        }

        private void BindGroupByParameters(DataModel? dataModel, HttpContext context, Dictionary<string, object> formDatas)
        {
            if (dataModel?.GroupParemters != null)
            {
                var groupBys = formDatas.FirstOrDefault(it => it.Key.EqualsCase(nameof(DataModel.GroupParemters)));
                if (groupBys.Value != null)
                {
                    dataModel!.GroupParemters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataModelGroupParameter>>(groupBys.Value + "");
                }
            }
        }

        private void BindOrderByParameters(DataModel? dataModel, HttpContext context, Dictionary<string, object> formDatas)
        {
            if (dataModel?.OrderDynamicParemters != null)
            {
                //var data = dataModel?.DefaultParameters?.FirstOrDefault(it => it?.Name?.EqualsCase(nameof(DataModel.OrderByFixedParemters )) == true);
                //if (data != null)
                //{
                var orderDatas = formDatas.FirstOrDefault(it => it.Key.EqualsCase(nameof(DataModel.OrderDynamicParemters)));
                if (orderDatas.Value != null)
                {
                    dataModel!.OrderDynamicParemters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataModelDynamicOrderParemter>>(orderDatas.Value + "");
                }
                //}
            }
        }
        private void BindPageParameters(DataModel? dataModel, HttpContext context, Dictionary<string, object> formDatas)
        {
            if (dataModel?.CommonPage != null)
            {
                var data = dataModel?.DefaultParameters?.FirstOrDefault(it => it?.Name?.EqualsCase(nameof(DataModelPageParameter.PageNumber)) == true);
                if (data != null)
                {
                    data.Value = GetParameterValueFromRequest(data, context, formDatas);
                    dataModel!.CommonPage.PageNumber = Convert.ToInt32(data.Value ?? "1");
                }
                var data2 = dataModel?.DefaultParameters?.FirstOrDefault(it => it?.Name?.EqualsCase(nameof(DataModelPageParameter.PageSize)) == true);
                if (data2 != null)
                {
                    data2.Value = GetParameterValueFromRequest(data2, context, formDatas);
                    dataModel!.CommonPage.PageSize = Convert.ToInt32(data2.Value ?? "1");
                }
            }
        }

        private void BindDefaultParameters(DataModel? dataModel, HttpContext context, Dictionary<string, object> formDatas)
        {
            if (IsJObjct(dataModel, formDatas))
            {
                var data = dataModel?.DefaultParameters?.FirstOrDefault();
                if (data != null)
                {
                    data.Value = App.Db.Utilities.SerializeObject(formDatas);
                }
            }
            else if (dataModel!.DefaultParameters != null)
            {
                dataModel!.DefaultParameters = dataModel?.DefaultParameters?.Where(it => NoPageParameters(it)).ToList();
                foreach (var item in dataModel?.DefaultParameters ?? new List<DataModelDefaultParameter>())
                {
                    UpdateWhereItemValue(context, formDatas, item);
                }
            }
        }

        private static bool IsJObjct(DataModel? dataModel, Dictionary<string, object> formDatas)
        {
            var isJObject = dataModel?.DefaultParameters?.Count == 1 && dataModel!.DefaultParameters!.First().ValueType == nameof(JObject) && dataModel!.DefaultParameters!.First().IsSingleParameter == true;
            return isJObject;
        }

        private void UpdateWhereItemValue(HttpContext context, Dictionary<string, object> formDatas, DataModelDefaultParameter item)
        {
            item.Value = GetParameterValueFromRequest(item, context, formDatas);
            if (IsDefaultValue(item))
            {
                item.Value = item.DefaultValue;
            }
            if (IsUserName(item))
            {
                var options = SuperAPIModule._apiOptions;
                item.Value = options?.DatabaseOptions!.GetCurrentUserCallback().UserName;
            }
            else if (IsDateTimeNow(item))
            {
                var options = SuperAPIModule._apiOptions;
                item.Value = DateTime.Now;
            }
            else if (IsFile(item))
            {
                item.Value = PubMethod.ConvertFromBase64(item.Value + "");
            }
            //if (!string.IsNullOrEmpty(item?.FieldName))
            //{
            //    item.Name = item.FieldName;
            //}
        }



        private static bool NoPageParameters(DataModelDefaultParameter it)
        {
            return it.Name != nameof(DataModelPageParameter.PageNumber) &&
                        it.Name != nameof(DataModelPageParameter.PageSize);
        }

        private static bool IsUserName(DataModelDefaultParameter item)
        {
            return item?.InsertParameter?.IsUserName == true;
        }
        private static bool IsDateTimeNow(DataModelDefaultParameter item)
        {
            return item?.InsertParameter?.IsDateTimeNow == true;
        }
        private bool IsFile(DataModelDefaultParameter item)
        {
            return item?.ValueType == "Byte[]";
        }
        private static bool IsDefaultValue(DataModelDefaultParameter item)
        {
            return item.Value == null && item.DefaultValue != null;
        }

        private string GetParameterValueFromRequest(DataModelDefaultParameter parameter, HttpContext context, Dictionary<string, object> formDatas)
        {
            if (parameter.ValueIsReadOnly)
            {
                return parameter.Value + "";
            }
            string parameterValue = context.Request.Query[parameter.Name];
            var formData = formDatas.FirstOrDefault(it => it.Key.EqualsCase(parameter.Name ?? ""));
            if (formData.Key != null)
            {
                parameterValue = formData.Value + "";
            }
            parameter.Value = parameterValue;
            return parameterValue;
        }

        private static Dictionary<string, object> GetFormDatas(HttpContext context)
        {

            Dictionary<string, object> formDatas = new Dictionary<string, object>();
            if (context.Request.Body != null)
            {
                AddFormData(context, formDatas);
                AddRawParameters(context, formDatas);
            }
            return formDatas ?? new Dictionary<string, object>();
        }

        private static void AddFormData(HttpContext context, Dictionary<string, object> formDatas)
        {
            if (IsFormData(context))
            {
                var formParams = context.Request.Form;

                foreach (var key in formParams.Keys)
                {
                    formDatas[key] = formParams[key];
                }
            }
        }

        private static StreamReader AddRawParameters(HttpContext context,  Dictionary<string, object> formDatas)
        {
            using StreamReader reader = new System.IO.StreamReader(context.Request.Body);
            var body = reader.ReadToEndAsync().Result;

            if (!string.IsNullOrEmpty(body))
            {

                var bodyParams = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(body) ?? new Dictionary<string, object>();

                var items = formDatas.Union(bodyParams).ToDictionary(pair => pair.Key, pair => pair.Value);
                foreach (var item in items)
                {
                    formDatas.Add(item.Key,item.Value);
                }
            }

            return reader;
        }

        private static bool IsFormData(HttpContext context)
        {
            var contentTypes = new List<string>()
            {
                "multipart/form-data",
                "application/x-www-form-urlencoded"
            };
            return context.Request.ContentType != null && contentTypes.Any(context.Request.ContentType.Contains);
        }
    }
}
