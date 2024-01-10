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
            var result = items.GroupBy(it => it.TextGroup).Select(it => new
            {
                TextGroup = it.Key,
                Items = it.ToList()
            }).ToList();
            return result;
        }
    }
}
