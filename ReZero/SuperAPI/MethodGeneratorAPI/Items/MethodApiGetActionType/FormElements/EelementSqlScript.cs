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
            RemoveCommonItem(result);
            result.Insert(3,new ActionTypeFormElementModel()
            {
                Text="SQL脚本",
                ElementType = ElementType.SqlText,
                Name = nameof(SaveInterfaceListModel.ActionType),
                Value = "select * from tableName  where  id={int:1} "
            });
            return result;
        }

        private static void RemoveCommonItem(List<ActionTypeFormElementModel> result)
        {
            result.RemoveAll(it => it.Name == "TableId");
        }
    }
}
