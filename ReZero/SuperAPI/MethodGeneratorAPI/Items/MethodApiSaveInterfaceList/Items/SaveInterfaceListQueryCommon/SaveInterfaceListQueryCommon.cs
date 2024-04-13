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
        public ZeroEntityInfo? zeroEntityInfo { get; set; } 
        public bool isPage { get; set; }
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            SetCurrentClassField(zeroInterfaceList, saveInterfaceListModel);
            SetChildObject(zeroInterfaceList);
            SetPage(saveInterfaceListModel, zeroInterfaceList);
            SetColumns(saveInterfaceListModel, zeroInterfaceList);
            SetOrderBy(saveInterfaceListModel, zeroInterfaceList);
            SetWhere(saveInterfaceListModel, zeroInterfaceList);
            return SaveData(zeroInterfaceList);
        }
         

        private void SetCurrentClassField(ZeroInterfaceList zeroInterfaceList, SaveInterfaceListModel saveInterfaceListModel)
        {
            var tableId=Convert.ToInt64( zeroInterfaceList.DataModel!.TableId);
            var db = App.Db;
            this.zeroEntityInfo = db.Queryable<ZeroEntityInfo>()
                .Includes(it => it.ZeroEntityColumnInfos).Where(it=>it.Id==tableId).First();
            this.isPage = saveInterfaceListModel?.PageSize == true;
        }

        private static void SetChildObject(ZeroInterfaceList zeroInterfaceList)
        {
            zeroInterfaceList.DataModel!.DefaultParameters = zeroInterfaceList.DataModel.DefaultParameters ?? new List<DataModelDefaultParameter>();
        } 
    }
}
