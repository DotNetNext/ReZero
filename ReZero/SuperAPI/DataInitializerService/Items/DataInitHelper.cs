using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class DataInitHelper
    {

        public static DataModelDefaultParameter GetIsInitializedParameter()
        {
            return new DataModelDefaultParameter() { Name = "IsInitialized", ValueIsReadOnly = true, Value = true, ValueType = typeof(bool).Name, Description = TextHandler.GetCommonText("是否内置数据", "Is initialized") };
        }
        public static DataModelDefaultParameter GetIsDynamicParameter()
        {
            return new DataModelDefaultParameter() { Name = "IsInitialized", ValueIsReadOnly = true, Value = false, ValueType = typeof(bool).Name, Description = TextHandler.GetCommonText("是否内置数据", "Is initialized") };
        }
    }
}
