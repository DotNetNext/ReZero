using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementBizDeleteObject : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            AddInterfaceName(result);
            AddInterfacUrl(result);
            AddGroup(result);
            AddTable(result);
            return result;
        } 
    }
}
