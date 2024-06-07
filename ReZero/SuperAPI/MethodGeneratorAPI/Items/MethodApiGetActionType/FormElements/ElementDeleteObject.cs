using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementDeleteObject : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            result.Insert(4, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.UpdateResultType,
                Name = "ResultType",
                Text = TextHandler.GetCommonText("返回类型", "return type")
            });
            return result;
        } 
    }
}
