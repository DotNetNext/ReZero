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
                Text = TextHandler.GetCommonText("Url", "Url"),
                ElementType = ElementType.Text,
                IsRequired = true
            });
        }

        protected void AddInterfaceName(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(ZeroInterfaceList.Name),
                Text= TextHandler.GetCommonText("接口名称","Interface name"),
                ElementType = ElementType.Text,
                IsRequired = true
            });
        }

        protected void AddTable(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(DataModel.TableId),
                Text = TextHandler.GetCommonText("表名", "Table name"),
                ElementType = ElementType.Text 
            });
        }

        protected void AddGroup(List<ActionTypeFormElementModel> result)
        {
            result.Add(new ActionTypeFormElementModel()
            {
                Name = nameof(ZeroInterfaceList.GroupName),
                Text = TextHandler.GetCommonText("分组名", "Group name"),
                ElementType = ElementType.Text,
                IsRequired = true
            });
        }
    }
}
