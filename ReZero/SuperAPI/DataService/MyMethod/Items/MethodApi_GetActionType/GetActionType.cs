using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public object GetActionType()
        {
            var items = EnumAttributeExtractor.GetEnumAttributeValues<ActionType>();
            var result = items!
                .GroupBy(it => it.TextGroup)!
                .Select(it => GetActionTypeSelectItem(it))
                .ToList();
            return result;
        }

        private static ActionTypeModel GetActionTypeSelectItem(IGrouping<string?, EnumAttributeExtractor.EnumAttributeValues> it)
        {
            return new ActionTypeModel
            {
                TextGroup = it.Key,
                Items = GetItemResult(it)
            };
        }

        private static List<ActionTypeItemModel> GetItemResult(IGrouping<string?, EnumAttributeExtractor.EnumAttributeValues> it)
        {
            var items = new List<ActionTypeItemModel>();
            foreach (var item in it.ToList())
            {
                items.Add(new ActionTypeItemModel()
                {
                     Text=item.Text,
                     TextGroup=item.TextGroup, 
                     FormElements= GetFormElements(item),
                });
            }
            return items;
        }

        private static List<ActionTypeFormElementModel> GetFormElements(EnumAttributeExtractor.EnumAttributeValues item)
        {
            var actionType = (ActionType)item.Value;
            var fullName = InstanceManager.GetActionTypeElementName(actionType);
            var type = Type.GetType(fullName);
            if (type == null) return new List<ActionTypeFormElementModel>();
            var actionInstance = (IEelementActionType)Activator.CreateInstance(type); 
            var result=actionInstance.GetModels();
            return result;
        }
    }
}
