using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    public class BaseElement
    {
        protected void AddInterfacUrl(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(ZeroInterfaceList.Url),
                ElementType = ElementType.Text,
                IsRequiRed = true
            });
        }

        protected void AddInterfaceName(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(ZeroInterfaceList.Name),
                ElementType = ElementType.Text,
                IsRequiRed = true
            });
        }

        protected void AddTable(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(DataModel.TableId),
                ElementType = ElementType.Text,
                IsRequiRed = true
            });
        }

        protected void AddGroup(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(ZeroInterfaceList.GroupName),
                ElementType = ElementType.Text,
                IsRequiRed = true
            });
        }
    }
}
