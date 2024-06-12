using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementQueryTree : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            result.Insert(3, new ActionTypeFormElementModel()
            {
                 ElementType= ElementType.Text,
                 Name= "TreeCode",
                 Text=TextHandler.GetCommonText("编号字段名", "Number field name"),
                 IsRequired=true,
            });
            result.Insert(4, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "TreeParentCode",
                Text = TextHandler.GetCommonText("父级编号字段名", "Parent number Field name"),
                IsRequired = true,
            });
            result.Insert(5, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Where,
                Name = "Where",
                Text = TextHandler.GetCommonText("条件", "Where"),
                Placeholder="",
                IsRequired = true,
            });
            return result;
        } 
    }
}
