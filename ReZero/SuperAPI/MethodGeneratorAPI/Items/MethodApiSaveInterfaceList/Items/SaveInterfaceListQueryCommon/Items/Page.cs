using System;
using System.Collections.Generic;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {

        private static void SetPage(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            if (saveInterfaceListModel.PageSize)
            {
                zeroInterfaceList!.DataModel!.CommonPage = new DataModelPageParameter
                {
                    PageSize = 20,
                    PageNumber = 1
                };
                zeroInterfaceList.DataModel.DefaultParameters!.AddRange(
                  new List<DataModelDefaultParameter>() {
                             new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageNumberPropName ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                             new DataModelDefaultParameter() { Name=SuperAPIModule._apiOptions?.InterfaceOptions.PageSizePropName ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") }
                    }
                  );
                zeroInterfaceList.CustomResultModel = new ResultModel()
                {
                    ResultType = ResultType.Grid
                };
            }
        }
    }
}
