using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ElementMethodGeneratorAPI : BaseElement, IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            RemoveCommonItem(result);
            result.Insert(3, new ActionTypeFormElementModel()
            {
                Text = TextHandler.GetCommonText("C#文本", "C# code text"),
                ElementType = ElementType.CSharpText,
                Name = "CSharpText",
                Value = "  "
            });
            return result; 
        }
        private static void RemoveCommonItem(List<ActionTypeFormElementModel> result)
        {
            result.RemoveAll(it => it.Name == "TableId");
        }
    }
}
