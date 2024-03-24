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
        public ZeroEntityInfo? zeroEntityInfo { get;set;}
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            SetCurrentClassField(zeroInterfaceList);
            SetChildObject(zeroInterfaceList);
            SetPage(saveInterfaceListModel, zeroInterfaceList);
            SetColumns(saveInterfaceListModel, zeroInterfaceList);
            SetOrderBy(saveInterfaceListModel, zeroInterfaceList);
            SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return InsertData(zeroInterfaceList);
        }

        private void SetCurrentClassField(ZeroInterfaceList zeroInterfaceList)
        {
            var tableId=Convert.ToInt64( zeroInterfaceList.DataModel!.TableId);
            var db = App.Db;
            this.zeroEntityInfo = db.Queryable<ZeroEntityInfo>()
                .Includes(it => it.ZeroEntityColumnInfos).First();
        }

        private static void SetChildObject(ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DefaultParameters = zeroInterfaceList.DataModel.DefaultParameters ?? new List<DataModelDefaultParameter>();
        } 
    }
}
