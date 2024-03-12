using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using Newtonsoft.Json;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            SetChildObject(zeroInterfaceList);
            SetPage(saveInterfaceListModel, zeroInterfaceList); 
            SetColumns(saveInterfaceListModel, zeroInterfaceList);
            SetOrderBy(saveInterfaceListModel, zeroInterfaceList);
            SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return base.InsertData(zeroInterfaceList);
        }

        private void SetColumns(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
    
        }

        private void SetWhere(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
            
        }

        private void SetOrderBy(SaveInterfaceListModel saveInterfaceListModel, ZeroInterfaceList zeroInterfaceList)
        {
        
        }

        private static void SetChildObject(ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DefaultParameters = zeroInterfaceList.DataModel.DefaultParameters ?? new List<DataModelDefaultParameter>();
        }

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
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageNumber) ,Value=1,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("第几页", "Page number") },
                             new DataModelDefaultParameter() { Name=nameof(DataModelPageParameter.PageSize) ,Value=20,FieldOperator=FieldOperatorType.Equal,  ValueType = typeof(long).Name, Description = TextHandler.GetCommonText("每页几条", "Pageize") }
                    }
                  );
            }
        }
    }
}
