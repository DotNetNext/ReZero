using Microsoft.AspNetCore.Http;
 using ReZero.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace ReZero.SuperAPI
{
    [Api(InterfaceCategoryInitializerProvider.Id100003)]
    internal class InternalInitApi
    {
        [DI]
        public IHttpContextAccessor? contextAccessor { get; set; }
        [ApiMethod(nameof(InternalInitApi.SaveLoginConfig), GroupName = PubConst.InitApi_SystemCommon, Url = PubConst.InitApi_SystemSaveConfig)]
        public bool SaveLoginConfig(bool enable)
        {
            var db = App.Db;
            var sysSetting = db.Queryable<ZeroSysSetting>().Where(it => it.TypeId == PubConst.Setting_EnableLoginType).First();
            if (sysSetting == null)
                sysSetting = new ZeroSysSetting() { Id = SqlSugar.SnowFlakeSingle.Instance.NextId(), TypeId = PubConst.Setting_EnableLoginType };
            sysSetting.BoolValue = enable;
            db.Storageable(sysSetting).ExecuteCommand();
            return true;
        }

        [ApiMethod(nameof(InternalInitApi.GetLoginConfig), GroupName = PubConst.InitApi_SystemCommon, Url = PubConst.InitApi_SystemGetInitConfig)]
        public object GetLoginConfig()
        {
            var db = App.Db;
            var sysSetting = db.Queryable<ZeroSysSetting>().Where(it => it.TypeId == PubConst.Setting_EnableLoginType).First();
            if (sysSetting == null) return false;
            return sysSetting.BoolValue;
        }
        [ApiMethod(nameof(InternalInitApi.VerifyCode), GroupName = PubConst.InitApi_SystemCommon, Url = PubConst.InitApi_VerifyCode)]
        public string VerifyCode()
        {
            var bytes = VerifyCodeSugar.Create();
            var base64String = Convert.ToBase64String(bytes);
            return $"data:image/png;base64,{base64String}";
        }

        [ApiMethod(nameof(InternalInitApi.SaveUser), GroupName  = nameof(ZeroUserInfo), Url = PubConst.InitApi_SaveUser)]
        public bool SaveUser(ZeroUserInfo zeroUserInfo)
        {
            var db = App.Db;
            if (zeroUserInfo.Id == -1)
            {
                if (zeroUserInfo.Id == 1)
                {
                    throw new Exception(TextHandler.GetCommonText("初始数据无非删除", ""));
                }
                db.Deleteable<ZeroEntityInfo>().Where(it => it.IsInitialized == false).In(zeroUserInfo.Id).ExecuteCommand();
            }
            else if (zeroUserInfo.Id == 0)
            {
                db.Insertable(zeroUserInfo).ExecuteCommand();
            }
            else
            {
                db.Updateable(zeroUserInfo).ExecuteCommand();
            }
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.GetUserById), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_GetUserById)]
        public ZeroEntityInfo GetUserById(long id) 
        {
            var db = App.Db;
            return db.Queryable<ZeroEntityInfo>().InSingle(id);
        }
    }
}
