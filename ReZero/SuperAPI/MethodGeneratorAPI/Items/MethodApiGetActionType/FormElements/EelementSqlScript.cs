using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public class ElementSqlScript : BaseElement, IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            return result;
        }
    }
}
