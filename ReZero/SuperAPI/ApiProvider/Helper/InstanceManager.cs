using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ReZero.SuperAPI
{
    internal class InstanceManager
    {

        public static async Task WriteFileAsync(HttpContext context, ZeroInterfaceList interInfo, object data)
        {
            var fileBytes = (byte[])data;
            context.Response.ContentType = "application/octet-stream"; // Or the appropriate MIME type for your file
            context.Response.ContentLength = fileBytes.Length;
            var fileName = string.Format(interInfo!.CustomResultModel!.GroupName, DateTime.Now.ToString("yyyyMMddHHmmss"));
            var contentDisposition = new ContentDispositionHeaderValue("attachment");
            contentDisposition.FileName = fileName;
            context.Response.Headers.Add("Content-Disposition", contentDisposition.ToString());
            // Write the file bytes to the response body
            await context.Response.Body.WriteAsync(fileBytes, 0, fileBytes.Length);
        }
        public static async Task<bool> AuthorizationAsync(HttpContext context, InterfaceContext dynamicInterfaceContext)
        {
            if (SuperAPIModule._apiOptions!.InterfaceOptions!.NoAuthorizationFunc != null)
            {
                if (SuperAPIModule._apiOptions!.InterfaceOptions!.NoAuthorizationFunc(dynamicInterfaceContext) == true) 
                {
                    return true;
                } 
            }
            if (SuperAPIModule._apiOptions?.InterfaceOptions?.Jwt?.Enable != true)
            {  
                return true;
            }
            if (context.Request.Path.ToString()?.ToLower() == PubConst.Jwt_TokenUrl) 
            {
                return true;
            }
            if (SuperAPIModule._apiOptions?.InterfaceOptions?.Jwt?.DisableSystemInterface == true) 
            {
                if (dynamicInterfaceContext.InterfaceType == InterfaceType.SystemApi) 
                {
                    context.Response.StatusCode = 401;
                    throw new Exception(TextHandler.GetCommonText("系统接口被禁用无法访问，修改JWT参数DisableSystemInterface", "If the system interface is disabled and cannot be accessed, modify the JWT parameter DisableSystemInterface"));
                }
            } 
            var url = context.Request.Path.ToString().ToLower();
            if (url.StartsWith("/public/")) 
            {
                return true;
            }
            var jsonClaims = SuperAPIModule._apiOptions?.InterfaceOptions?.Jwt.Claim ?? new List<Configuration.ClaimItem>(); ;
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Split(' ')[1];
                try
                {
                    // 进行JWT令牌验证，例如使用Microsoft.AspNetCore.Authentication.JwtBearer包提供的验证器
                    var authResult = await context.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
                    if (authResult.Succeeded)
                    {
                        var claims = authResult.Principal.Claims.ToList(); 
                        foreach (var claim in claims)
                        {
                            object value = claim.Value;
                            var type=jsonClaims.FirstOrDefault(it => claim.Type?.ToLower() == it.FieldName?.ToLower())?.Type;
                            if (!string.IsNullOrEmpty(type)) 
                            {
                                value = UtilMethods.ConvertDataByTypeName(type,value+"");
                            }
                            dynamicInterfaceContext.AttachClaimToHttpContext(claim.Type, value);
                        }
                        if (claims.Any())
                        {
                            var userName = claims[0].Value;
                            var list=CacheManager<ZeroPermissionInfo>.Instance.GetList();
                            if (list.Any()) 
                            {
                                var mappings=CacheManager<ZeroPermissionMapping>.Instance.GetList();
                                if (!mappings
                                    .Where(it => it.UserName!.ToLower() == userName?.ToLower())
                                    .Where(it=> it.InterfaceId== dynamicInterfaceContext?.InterfaceInfo?.Id)
                                    .Any()) 
                                {
                                    throw new Exception(TextHandler.GetCommonText("当前用户在【接口授权】中没有配置可以访问的接口权限 或者 清空所有【接口授权】中的数据", "No interface permission is configured for the current user or the data in all interface permissions is cleared"));
                                }
                            }
                        }
                        return true;
                    }
                    else
                    { 
                        // 用户未通过身份验证，可能需要进行一些处理，例如返回未经授权的错误
                        context.Response.StatusCode = 401;
                        throw new Exception(TextHandler.GetCommonText("用户未通过身份验证", "The user is not authenticated"));
                    }
                }
                catch (Exception)
                {
                    // JWT验证失败
                    context.Response.StatusCode = 401;
                    throw new Exception(TextHandler.GetCommonText("JWT验证失败", "JWT authentication failed"));
                }
            }
            else
            {
                // Authorization标头缺失或格式不正确
                context.Response.StatusCode = 401;
                throw new Exception(TextHandler.GetCommonText("Authorization标头缺失或格式不正确", "The Authorization header is missing or incorrectly formatted"));
            }
        } 
        public static string GetActionTypeName(ActionType actionType)
        {
            return $"ReZero.SuperAPI.{actionType}";
        }
        public static string GetActionTypeName(DataModel dataModel)
        {
            return $"ReZero.SuperAPI.{dataModel.ActionType}";
        }
        public static string GetActionTypeElementName(ActionType actionType)
        {
            return $"ReZero.SuperAPI.Element{actionType}";
        }

        public static string GetSaveInterfaceType(ActionType actionType)
        {
            return $"ReZero.SuperAPI.SaveInterfaceList{actionType}";
        }
        public static void CheckActionType(DataModel dataModel, Type actionType)
        {
            if (actionType == null)
            {
                throw new ArgumentException($"Invalid ActionType: {dataModel.ActionType}");
            }
        }
    }
}
