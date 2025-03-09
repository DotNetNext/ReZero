using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace ReZero.SuperAPI 
{
    [Api(InterfaceCategoryInitializerProvider.Id100003)]
    internal class InternalInitApi
    {
        [ApiMethod(nameof(InternalInitApi.SaveLoginConfig), GroupName = PubConst.InitApi_SystemCommon, Url =PubConst.InitApi_SystemSaveConfig)]
        public bool SaveLoginConfig(bool enable) 
        {
            var db = App.Db;
            var sysSetting = db.Queryable<ZeroSysSetting>().Where(it => it.TypeId == PubConst.Setting_EnableLoginType).First();
            if (sysSetting == null)
                sysSetting = new ZeroSysSetting() { Id=SqlSugar.SnowFlakeSingle.Instance.NextId(),TypeId = PubConst.Setting_EnableLoginType };
            sysSetting.BoolValue = enable;
            db.Storageable(sysSetting).ExecuteCommand();
            return true;
        }

        [ApiMethod(nameof(InternalInitApi.GetLoginConfig), GroupName = PubConst.InitApi_SystemCommon, Url = PubConst.InitApi_SystemGetInitConfig)]
        public object GetLoginConfig()
        {
            var db = App.Db;
            var sysSetting=db.Queryable<ZeroSysSetting>().Where(it => it.TypeId == PubConst.Setting_EnableLoginType).First();
            if (sysSetting == null) return false;
            return sysSetting.BoolValue;
        }
    }
}
