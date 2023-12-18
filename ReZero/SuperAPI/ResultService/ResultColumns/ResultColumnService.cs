using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    internal class ResultColumnService
    { 
        internal object GetValue(object propertyValue, ResultColumnModel? resultColumnModel)
        {
            switch (resultColumnModel!.ResultColumnType) 
            {
                case ResultColumnType.DefaultConvert:
                    propertyValue= UtilMethods.ChangeType2(propertyValue, resultColumnModel.ConvertType);
                    return propertyValue;
            }
            return propertyValue;
        }
    }
}
