using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
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
            if (dataModel?.OrderParemters != null) {
                //var data = dataModel?.DefaultParameters?.FirstOrDefault(it => it?.Name?.EqualsCase(nameof(DataModel.OrderParemters)) == true);
                //if (data != null)
                //{
                    var orderDatas = formDatas.FirstOrDefault(it => it.Key.EqualsCase(nameof(DataModel.OrderParemters)));
                    if (orderDatas.Value != null)
                    {
                        dataModel!.OrderParemters = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DataModelOrderParemter>>(orderDatas.Value + "");
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
            var isJObject= dataModel?.DefaultParameters?.Count == 1 && dataModel!.DefaultParameters!.First().ValueType == nameof(JObject) && dataModel!.DefaultParameters!.First().IsSingleParameter==true;
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
                item.Value =DateTime.Now;
            }
            else if (IsFile(item))
            { 
                item.Value = PubMethod.ConvertFromBase64(item.Value+"");
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
           return item?.ValueType=="Byte[]";
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
            if (formData.Key!=null)
            {
                parameterValue = formData.Value + "";
            }
            parameter.Value = parameterValue; 
            return parameterValue;
        }

        private static Dictionary<string, object> GetForDatams(HttpContext context)
        {
            Dictionary<string, object> formDatas = new Dictionary<string, object>();
            if (context.Request.Body != null)
            {
                // 从请求正文中读取参数值
                using var reader = new System.IO.StreamReader(context.Request.Body);
                var body = reader.ReadToEndAsync().Result;
                if (!string.IsNullOrEmpty(body))
                {
                    formDatas = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(body) ?? new Dictionary<string, object>();

                }
            }
            return formDatas ?? new Dictionary<string, object>();
        }

    }
}
