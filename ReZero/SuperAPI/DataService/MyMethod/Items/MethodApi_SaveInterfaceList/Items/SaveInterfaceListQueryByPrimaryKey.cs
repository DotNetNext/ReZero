using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ReZero.SuperAPI
{
    public class SaveInterfaceListQueryByPrimaryKey : BaseSaveInterfaceList, ISaveInterfaceList
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            ZeroInterfaceList zeroInterfaceList = new ZeroInterfaceList();
            base.SetCommonProperties(zeroInterfaceList, saveInterfaceListModel);
            App.Db.Insertable(zeroInterfaceList).ExecuteReturnSnowflakeId();
            return true;
        }
    }
}
