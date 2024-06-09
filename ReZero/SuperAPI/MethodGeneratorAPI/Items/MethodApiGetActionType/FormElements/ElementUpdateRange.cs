using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementUpdateRange : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            result.Insert(3, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.DefaultValueColumn,
                Name = "DefaultValueColumns", 
                Text = TextHandler.GetCommonText("默认值", "Dafault value")
            });
            result.Insert(6, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.UpdateColumns,
                Name = "TableColumns",
                Value = "",
                Text = TextHandler.GetCommonText("更新的列 ( 默认所有 )", "Updated columns")
            });
            result.Insert(7, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.UpdateResultType,
                Name = "ResultType",
                Text = TextHandler.GetCommonText("返回类型", "return type")
            });
            return result;
        } 
    }
}
