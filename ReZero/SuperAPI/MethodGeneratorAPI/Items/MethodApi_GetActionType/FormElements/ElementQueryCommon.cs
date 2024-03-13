using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ReZero.SuperAPI;

namespace ReZero.SuperAPI
{
    public class ElementQueryCommon : BaseElement,IEelementActionType
    {
        public List<ActionTypeFormElementModel> GetModels()
        {
            var result = new List<ActionTypeFormElementModel>();
            base.AddActionTypeFormElementModels(result);
            base.AddActionTypeElementModel(result, this);
            this.AddElements(result);
            result = result.OrderBy(it => GetSprt(it)).ToList();
            return result;
        }

        private static int GetSprt(ActionTypeFormElementModel it)
        {
            if (it.ElementType == ElementType.Columns)
            {
                return 0;
            } 
            else if (it.ElementType == ElementType.Page)
            {
                return 0;
            }
            else if (it.ElementType == ElementType.Table)
            {
                return 0;
            } 
            else if (it.Name == nameof(ZeroInterfaceList.Name))
            {
                return -100;
            }
            else if (it.Name == nameof(ZeroInterfaceList.InterfaceCategoryId))
            {
                return -99;
            }
            else
            {
                return 1;
            }
        }

        private void AddElements(List<ActionTypeFormElementModel> result)
        {
 
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Columns,
                Name = "Columns",
                Text=TextHandler.GetCommonText("显示列", "Show columns"),
                Value = null
            });
           
            result.Add(new ActionTypeFormElementModel()
            {
                ElementType = ElementType.Page,
                Name = nameof(DataModel.CommonPage.PageSize),
                Text = TextHandler.GetCommonText("分页","Page"),
                Value =null
            }); 
        }
    }
}
