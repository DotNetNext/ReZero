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
                ElementType = ElementType.Text,
                Name = "Join",
                Text = TextHandler.GetCommonText("Join json", "Join json"),
                Value = null
            });
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "Where",
                Text=TextHandler.GetCommonText("Where json", "Where json"),
                Value = null
            });
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "OrderBy",
                Text = TextHandler.GetCommonText("OrderBy json", "OrderBy json"),
                Value = null
            });
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "Select",
                Text = TextHandler.GetCommonText("Select json", "Select json"),
                Value = null
            });
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = nameof(DataModel.CommonPage.PageSize),
                Text = TextHandler.GetCommonText("每页显示条数，不填不分页", "Page size"),
                Value =null
            }); 
        }
    }
}
