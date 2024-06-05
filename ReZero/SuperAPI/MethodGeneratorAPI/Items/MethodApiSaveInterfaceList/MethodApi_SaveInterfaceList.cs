using Newtonsoft.Json.Linq;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ReZero.SuperAPI
{
    public partial class MethodApi
    {
        public object SaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            var db = App.Db;
            ZeroInterfaceListCacheManager.Instance.ClearZeroInterfaceListCache();
            ISaveInterfaceList saveInterfaceList = GetSaveInterfaceList(saveInterfaceListModel);
            return saveInterfaceList.SaveInterfaceList(saveInterfaceListModel);
        }

        private static ISaveInterfaceList GetSaveInterfaceList(SaveInterfaceListModel saveInterfaceListModel)
        {
            var fullName= InstanceManager.GetSaveInterfaceType(saveInterfaceListModel.ActionType!.Value);
            var type=Type.GetType(fullName);
            return (ISaveInterfaceList)Activator.CreateInstance(type);
        }
    }
}
