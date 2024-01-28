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
            return result;
        } 
    }
}
