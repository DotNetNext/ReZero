using System;
using System.Collections.Generic;
using System.Text;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementQueryByPrimaryKey : IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            ActionTypeFormElementModel model = new ActionTypeFormElementModel()
            {
                Name = nameof(DataModel.TableId), 
                ElementType = ElementType.Table,
            };
            return new List<ActionTypeFormElementModel>();
        }
    }
}
