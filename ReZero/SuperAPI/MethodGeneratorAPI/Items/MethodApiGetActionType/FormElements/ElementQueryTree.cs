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
                 Name="Id",
                 Text=TextHandler.GetCommonText("编号字段名", "Number field name"),
                 IsRequired=true,
            });
            result.Insert(4, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "Id",
                Text = TextHandler.GetCommonText("父级编号字段名", "Parent number Field name"),
                IsRequired = true,
            });
            result.Insert(5, new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Text,
                Name = "Id",
                Text = TextHandler.GetCommonText("根目录ParentId的值", "Primary key of the root directory"),
                Placeholder="一般是0或者空，例如【Id=1,ParentId=0】，那么就是0",
                IsRequired = true,
            });
            return result;
        } 
    }
}
