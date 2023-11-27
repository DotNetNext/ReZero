using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI
{
    internal class DataInitHelper
    {

        public static WhereParameter GetIsInitializedParameter()
        {
            return new WhereParameter() { Name = "IsInitialized", ValueIsReadOnly = true, Value = true, ValueType = typeof(bool).Name, Description = TextHandler.GetCommonTexst("是否内置数据", "Is initialized") };
        }

    }
}
