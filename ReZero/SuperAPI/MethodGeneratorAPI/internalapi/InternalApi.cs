using Microsoft.AspNetCore.Http;
using ReZero.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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

        [ApiMethod(nameof(InternalInitApi.SaveUser), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_SaveUser)]
        public bool SaveUser(ZeroUserInfo zeroUserInfo)
        {
            var db = App.Db;
            if (zeroUserInfo?.Avatar?.StartsWith("data:image/") == true)
            {
                var avatarBytes = PubMethod.ConvertBase64ToBytes(zeroUserInfo.Avatar);
                var imgId = SqlSugar.SnowFlakeSingle.Instance.NextId();
                var avatarDirectory = Path.Combine(AppContext.BaseDirectory, "wwwroot\\Rezero\\Avatar");
                if (!Directory.Exists(avatarDirectory))
                {
                    Directory.CreateDirectory(avatarDirectory);
                }
                var avatarPath = Path.Combine(avatarDirectory, $"{imgId}.jpg");
                File.WriteAllBytes(avatarPath, avatarBytes);
                zeroUserInfo.Avatar = $"Avatar/{imgId}.jpg";
            }
            if (zeroUserInfo?.Id == 0)
            {
                if (string.IsNullOrEmpty(zeroUserInfo.UserName))
                {
                    throw new Exception(TextHandler.GetCommonText("用户名不能为空", "Username cannot be empty"));
                }
                if (string.IsNullOrEmpty(zeroUserInfo.Password))
                {
                    throw new Exception(TextHandler.GetCommonText("密码不能为空", "Password cannot be empty"));
                }
                if (db.Queryable<ZeroUserInfo>().Any(it => it.UserName == zeroUserInfo.UserName))
                {
                    throw new Exception("用户名已存在");
                }
                zeroUserInfo.CreateTime = DateTime.Now;
                zeroUserInfo.Creator = "admin";
                zeroUserInfo.CreatorId = 1;
                zeroUserInfo.Id = SqlSugar.SnowFlakeSingle.Instance.NextId();
                db.Insertable(zeroUserInfo).ExecuteCommand();
            }
            else
            {
                zeroUserInfo.Password = Encryption.Encrypt(zeroUserInfo.Password!);
                zeroUserInfo.Modifier = "admin";
                db.Updateable(zeroUserInfo).IgnoreColumns(true).ExecuteCommand();
            }
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.GetUserById), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_GetUserById)]
        public ZeroUserInfo GetUserById(long id) 
        {
            var db = App.Db;
            return db.Queryable<ZeroUserInfo>().InSingle(id);
        }
        [ApiMethod(nameof(InternalInitApi.DeleteUserInfo), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_DeleteUserById)]
        public bool DeleteUserInfo(long id) 
        {
            var db = App.Db;
            var zeroUser = db.Queryable<ZeroUserInfo>().InSingle(id);
            if (zeroUser == null) return true;
            if (zeroUser.IsInitialized||zeroUser.Id==1) 
            {
                throw new Exception("初始化数据无法删除");
            }
            db.Deleteable<ZeroUserInfo>().In(zeroUser.Id).ExecuteCommand();
            return true;
        }
    }
}
