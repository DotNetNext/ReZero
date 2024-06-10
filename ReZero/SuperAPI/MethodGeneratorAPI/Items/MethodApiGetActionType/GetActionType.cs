 using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
                var eles = GetFormElements(item);
                eles.Add(new  ActionTypeFormElementModel()
                {
                    
                    ElementType= ElementType.Select,
                    Name=nameof(HttpMethod), 
                    Text= nameof(HttpMethod),
                    SelectDataSource=new List<ActionTypeFormElementSelectDataSourceModel>() 
                    {
                        new ActionTypeFormElementSelectDataSourceModel()
                        {
                            Key=HttpRequestMethod.All.ToString().FirstCharToUpper(),
                            Value=HttpRequestMethod.All.ToString().FirstCharToUpper(),
                        },
                        new ActionTypeFormElementSelectDataSourceModel()
                        {
                            Key=HttpRequestMethod.GET.ToString().FirstCharToUpper(),
                            Value=HttpRequestMethod.GET.ToString().FirstCharToUpper(),
                        },
                        new ActionTypeFormElementSelectDataSourceModel()
                        {
                            Key=HttpRequestMethod.POST.ToString().FirstCharToUpper(),
                            Value=HttpRequestMethod.POST.ToString().FirstCharToUpper(),
                        },
                        new ActionTypeFormElementSelectDataSourceModel()
                        {
                           Key=HttpRequestMethod.PUT.ToString().FirstCharToUpper(),
                           Value=HttpRequestMethod.PUT.ToString().FirstCharToUpper(),
                        },
                        new ActionTypeFormElementSelectDataSourceModel()
                        {
                            Key=HttpRequestMethod.DELETE.ToString().FirstCharToUpper(),
                            Value=HttpRequestMethod.DELETE.ToString().FirstCharToUpper(),
                        },
                    }
                    
                });
                items.Add(new ActionTypeItemModel()
                {
                     Text=item.Text,
                     TextGroup=item.TextGroup, 
                     FormElements= eles,
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
