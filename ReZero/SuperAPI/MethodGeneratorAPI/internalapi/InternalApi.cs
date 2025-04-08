using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ReZero.DependencyInjection;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;

namespace ReZero.SuperAPI
{
    [Api(InterfaceCategoryInitializerProvider.Id100003)]
    internal class InternalInitApi
    {
        [DI]
        public IHttpContextAccessor? contextAccessor { get; set; }

        #region Setting
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

        #endregion

        #region User
        public object VerifyCode()
        {
            var bytes = VerifyCodeSugar.Create();
            var base64String = Convert.ToBase64String(bytes.Item2);
            return new { Code = Encryption.Encrypt(bytes.Item1?.ToLower() ?? string.Empty), Src = $"data:image/png;base64,{base64String}" };
        }
        [ApiMethod(nameof(InternalInitApi.SaveUser), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_SaveUser)]
        public bool SaveUser(ZeroUserInfo zeroUserInfo)
        {
            var db = App.Db;
            if (zeroUserInfo?.Avatar?.StartsWith("data:image/") == true)
            {
                var avatarBytes = PubMethod.ConvertBase64ToBytes(zeroUserInfo.Avatar);
                var imgId = SqlSugar.SnowFlakeSingle.Instance.NextId();
                var avatarDirectory = Path.Combine(AppContext.BaseDirectory, SuperAPIStaticFileMiddleware.GetFilePathByCurrentDirectory(Path.Combine("images", "users")));
                if (!Directory.Exists(avatarDirectory))
                {
                    Directory.CreateDirectory(avatarDirectory);
                }
                var avatarPath = Path.Combine(avatarDirectory, $"{imgId}.jpg");
                File.WriteAllBytes(avatarPath, avatarBytes);
                zeroUserInfo.Avatar = $"images/users/{imgId}.jpg";
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
                    throw new Exception(TextHandler.GetCommonText("用户名已存在", "The user name already exists"));
                }
                zeroUserInfo.CreateTime = DateTime.Now;
                zeroUserInfo.Creator = "admin";
                zeroUserInfo.CreatorId = 1;
                zeroUserInfo.Password = Encryption.Encrypt(zeroUserInfo.Password);
                zeroUserInfo.Id = SqlSugar.SnowFlakeSingle.Instance.NextId();
                db.Insertable(zeroUserInfo).ExecuteCommand();
            }
            else
            {
                zeroUserInfo!.Password = Encryption.Encrypt(zeroUserInfo.Password!);
                zeroUserInfo.Modifier = "admin";
                db.Updateable(zeroUserInfo).IgnoreColumns(true).ExecuteCommand();
            }
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.GetUserById), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_GetUserById)]
        public ZeroUserInfo GetUserById(long id)
        {
            var db = App.Db;
            if (id == -1) 
            {
                var userName = DependencyResolver.GetLoggedInUser(); 
                var userInfo = App.Db.Queryable<ZeroUserInfo>().Where(it => it.UserName == userName || it.BusinessAccount == userName)
                    .First();
                return userInfo;
            }
            return db.Queryable<ZeroUserInfo>().InSingle(id);
        }
        [ApiMethod(nameof(InternalInitApi.DeleteUserInfo), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_DeleteUserById)]
        public bool DeleteUserInfo(long id)
        {
            var db = App.Db;
            var zeroUser = db.Queryable<ZeroUserInfo>().InSingle(id);
            if (zeroUser == null) return true;
            if (zeroUser.IsInitialized || zeroUser.Id == 1)
            {
                throw new Exception("初始化数据无法删除");
            }
            db.Deleteable<ZeroUserInfo>().In(zeroUser.Id).ExecuteCommand();
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.GetUserInfo), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_GetCurrentUser)]
        public object GetUserInfo()
        {
            var userName = DependencyResolver.GetLoggedInUser();
            var defaultSrc = "images/users/avatar.jpg";
            var defaultUserName = "ReZero"; 
            var userInfo = App.Db.Queryable<ZeroUserInfo>().Where(it => it.UserName == userName || it.BusinessAccount == userName)
                .First();
            if (userInfo?.Avatar==string.Empty)
            {
                userInfo.Avatar = defaultSrc;
            }
            if (userInfo == null) 
            {
                userInfo = new ZeroUserInfo()
                {
                     Avatar= defaultSrc
                }; 
            }
            return new { IsAdmin= userInfo.IsMasterAdmin, UserName = userInfo?.UserName?? defaultUserName, Avatar = userInfo?.Avatar };
        }
        [ApiMethod(nameof(InternalInitApi.GetBizUsers), GroupName = nameof(ZeroUserInfo), Url = PubConst.InitApi_GetBizUsers)]
        public object GetBizUsers()
        {
            var db = App.Db;
            var options = SuperAPIModule._apiOptions;
            var jwt = options?.InterfaceOptions?.Jwt ?? new Configuration.ReZeroJwt();
            var isEnable=options?.InterfaceOptions?.Jwt?.Enable==true;
            if (string.IsNullOrEmpty(jwt?.UserTableName)|| string.IsNullOrEmpty(jwt?.PasswordFieldName)|| string.IsNullOrEmpty(jwt?.UserNameFieldName)) 
            {
                throw new Exception(TextHandler.GetCommonText("JWT用户表或者字段未设置", "The JWT user table or field is not set"));
            }
            try
            {
                var result = db.Queryable<object>().AS(jwt.UserTableName)
                        .Select<string>(SelectModel.Create(
                            new SelectModel() { FieldName = jwt.UserNameFieldName, AsName = "username" }
                            )).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(TextHandler.GetCommonText(ex.Message, ex.Message));
            }
        }
        #endregion

        #region Entity
        [ApiMethod(nameof(InternalInitApi.ExecTemplateByViewWithoutCreatingFiles), GroupName = nameof(ZeroEntityInfo), Url = PubConst.InitApi_ViewTemplate)]
        public string ExecTemplateByViewWithoutCreatingFiles(long databaseId,bool isView, string viewName, long templateId) 
        {
            return new MethodApi().ExecTemplateByViewWithoutCreatingFiles(databaseId, viewName, isView, templateId);
        }
        #endregion

        #region Token
        [ApiMethod(nameof(InternalInitApi.AddTokenManage), GroupName = nameof(ZeroJwtTokenManagement), Url = PubConst.InitApi_AddTokenManage)]
        public bool AddTokenManage(ZeroJwtTokenManagement zeroJwtTokenManagement) 
        {
            CacheManager<ZeroInterfaceList>.Instance.ClearCache();
            var db = App.Db;
            var options = SuperAPIModule._apiOptions;
            var jwt = options?.InterfaceOptions?.Jwt ?? new Configuration.ReZeroJwt();
            if (string.IsNullOrEmpty(jwt.UserTableName) || string.IsNullOrEmpty(jwt.PasswordFieldName) || string.IsNullOrEmpty(jwt.UserNameFieldName)) 
            {
                throw new Exception(TextHandler.GetCommonText($"JWT信息没有配置完整表名字段名存在空", $"The JWT information is not fully configured. Table name The field name is empty"));
            }
            if (string.IsNullOrEmpty(zeroJwtTokenManagement.UserName))
            {
                throw new Exception(TextHandler.GetCommonText($"用户名必填", $"User name is required"));
            }
            if (zeroJwtTokenManagement.Expiration == DateTime.MinValue) 
            {
                throw new Exception(TextHandler.GetCommonText($"使用期限必填", $"The usage period is required"));
            }
            DataTable dt = new DataTable(); 
            try
            {
                dt = db.Queryable<object>()
                  .AS(jwt.UserTableName)
                  .Where(jwt.UserNameFieldName, "=", zeroJwtTokenManagement.UserName)
                  .ToDataTable(); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (dt.Rows.Count == 0) 
            {
                throw new Exception(TextHandler.GetCommonText($"JWT用户表没有找到{zeroJwtTokenManagement.UserName}", $" JWT user table not found {zeroJwtTokenManagement.UserName}"));
            }
            var password = dt.Rows[0][jwt.PasswordFieldName] + "";
            var token = new MethodApi() {  TokenExpiration=zeroJwtTokenManagement.Expiration }.GetToken(zeroJwtTokenManagement.UserName!,password);
            zeroJwtTokenManagement.CreateTime = DateTime.Now;
            zeroJwtTokenManagement.Creator = "admin";
            zeroJwtTokenManagement.Id = SqlSugar.SnowFlakeSingle.Instance.NextId();
            zeroJwtTokenManagement.Token = token;
            db.Insertable(zeroJwtTokenManagement).ExecuteCommand();
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.UpdateTokenManage), GroupName = nameof(ZeroJwtTokenManagement), Url = PubConst.InitApi_UpdateTokenManage)]
        public bool UpdateTokenManage(ZeroJwtTokenManagement zeroJwtTokenManagement)
        {
            CacheManager<ZeroInterfaceList>.Instance.ClearCache();
            var db = App.Db;
            zeroJwtTokenManagement.UpdateTime = DateTime.Now;
            db.Updateable(zeroJwtTokenManagement)
                .UpdateColumns(it => new { it.Description,it.EasyDescription ,it.UpdateTime}).ExecuteCommand();
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.DeleteTokenManage), GroupName = nameof(ZeroJwtTokenManagement), Url = PubConst.InitApi_DeleteTokenManage)]
        public bool DeleteTokenManage(long Id)
        {
            CacheManager<ZeroInterfaceList>.Instance.ClearCache();
            var db = App.Db;
            db.Updateable<ZeroJwtTokenManagement>()
                .SetColumns(it => it.IsDeleted == true)
                .Where(it => it.Id == Id).ExecuteCommand();
            return true;
        }
        [ApiMethod(nameof(InternalInitApi.GetZeroJwtTokenManagementById), GroupName = nameof(ZeroJwtTokenManagement), Url = PubConst.InitApi_GetTokenManageById)]
        public ZeroJwtTokenManagement GetZeroJwtTokenManagementById(long id) 
        {
            var data = App.Db.Queryable<ZeroJwtTokenManagement>().InSingle(id);
            return data;
        }
        #endregion
    }
}
