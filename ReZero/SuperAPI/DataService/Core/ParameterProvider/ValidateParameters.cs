using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal class ValidateParameters
    {
        public static List<ErrorParameter> Check(DataModel dataModel)
        {
            List<ErrorParameter> errorLists = new List<ErrorParameter>();
            foreach (var item in dataModel.DefaultParameters ?? new List<DefaultParameter>())
            {
                if (item?.ParameterValidate?.IsRequired == true && string.IsNullOrEmpty(item.Value + ""))
                {
                    errorLists.Add(new ErrorParameter() { Name = item.Name, ErrorType = "IsRequired", Message = TextHandler.GetCommonTexst("必填", "Required") });
                }
            }
            return errorLists;
        }
    }
}
