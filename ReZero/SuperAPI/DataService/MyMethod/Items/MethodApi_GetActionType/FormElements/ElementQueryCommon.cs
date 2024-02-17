using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementQueryCommon : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            this.AddElements(result);
            return result;
        }

        private void AddElements(List<ActionTypeFormElementModel> result)
        {
 
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Columns,
                Name = "Columns",
                Text=TextHandler.GetCommonText("显示列", "Show columns"),
                Value = null
            });
           
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Page,
                Name = nameof(DataModel.CommonPage.PageSize),
                Text = TextHandler.GetCommonText("分页","Page"),
                Value =null
            }); 
        }
    }
}
