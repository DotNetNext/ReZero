using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using Newtonsoft.Json;
using SqlSugar;
using SqlSugar.Extensions; 
namespace ReZero.SuperAPI 
{
    public partial class SaveInterfaceListQueryCommon : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            SetChildObject(zeroInterfaceList);
            SetPage(saveInterfaceListModel, zeroInterfaceList);
            SetColumns(saveInterfaceListModel, zeroInterfaceList);
            SetOrderBy(saveInterfaceListModel, zeroInterfaceList);
            SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return InsertData(zeroInterfaceList);
        }
         
        private static void SetChildObject(ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DefaultParameters = zeroInterfaceList.DataModel.DefaultParameters ?? new List<DataModelDefaultParameter>();
        } 
    }
}
