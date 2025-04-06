using Kdbndp.TypeHandlers;
using Microsoft.IdentityModel.Tokens;
using ReZero.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace ReZero.SuperAPI 
{
    public partial class MethodApi
    {
        public DateTime? TokenExpiration { get; set; } 

        /// <summary>
        /// 获取JWT Token
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>JWT Token字符串</returns>
        public string GetToken(string userName, string password)
        {
            var db = App.Db;
            var options = SuperAPIModule._apiOptions;
            var jwt = options?.InterfaceOptions?.Jwt ?? new Configuration.ReZeroJwt();
            ZeroUserInfo data = GetAdminUserInfo(userName, password, db);
            if (data != null && string.IsNullOrEmpty(data.BusinessAccount))
            { 
                return GenerateJwtToken(data, jwt);
            }
            else if (data != null&& !string.IsNullOrEmpty(data.BusinessAccount)) 
            {  
                var dt = db.Queryable<object>()
                    .AS(jwt.UserTableName)
                    .Where(jwt.UserNameFieldName, "=", data.BusinessAccount)
                    .ToDataTable();
                if (dt.Rows.Count == 0)
                {
                    throw new Exception(TextHandler.GetCommonText("授权失败", "Authorization failure"));
                }
                return GenerateJwtToken(dt.Rows[0], jwt); 
            }
            else //业务表登录
            {
                CheckJwt(jwt); 
                DataTable dt = new DataTable();
                try
                {
                    dt = db.Queryable<object>()
                       .AS(jwt.UserTableName)
                       .Where(jwt.PasswordFieldName, "=", password)
                       .Where(jwt.UserNameFieldName, "=", userName)
                       .ToDataTable();

                }
                catch (Exception)
                {
                    throw new Exception(TextHandler.GetCommonText("授权失败", "Authorization failure"));
                }
                if (dt.Rows.Count == 0)
                {
                    throw new Exception(TextHandler.GetCommonText("授权失败", "Authorization failure"));
                }
                return GenerateJwtToken(dt.Rows[0], jwt);
            }
        }

        /// <summary>
        /// 获取管理员用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="db">数据库连接</param>
        /// <returns>管理员用户信息</returns>
        private static ZeroUserInfo GetAdminUserInfo(string userName, string password, ISqlSugarClient db)
        {
            // 先验证是不是系统管理员账号
            return db.Queryable<ZeroUserInfo>()
            .Where(it => it.UserName == userName)
            .Where(it => it.Password == password).First();
        }

        /// <summary>
        /// 检查JWT配置
        /// </summary>
        /// <param name="jwt">JWT配置</param>
        private static void CheckJwt(ReZeroJwt jwt)
        {
            if (string.IsNullOrEmpty(jwt.Secret) || string.IsNullOrEmpty(jwt.UserTableName) || string.IsNullOrEmpty(jwt.UserTableName) || string.IsNullOrEmpty(jwt.UserTableName))
            {
                throw new Exception(TextHandler.GetCommonText("请到json文件配置jwt信息", "Go to the json file to configure the jwt information"));
            }
        }

        /// <summary>
        /// 生成JWT Token
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="jwt">JWT配置</param>
        /// <returns>JWT Token字符串</returns>
        private string GenerateJwtToken(ZeroUserInfo user, ReZeroJwt jwt)
        {
            var options = SuperAPIModule._apiOptions;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwt.Secret);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            foreach (var claim in jwt.Claim ?? new List<Configuration.ClaimItem>())
            {
                claims.Add(new Claim(claim.Key, user.GetType().GetProperty(claim.FieldName)?.GetValue(user, null)?.ToString() ?? ""));
            }
            var tokenExpiration = this.TokenExpiration;
            if (tokenExpiration == null) 
            {
                tokenExpiration = DateTime.UtcNow.AddMinutes(jwt?.Expires ?? 1000);
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = tokenExpiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// 生成JWT Token
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <param name="jwt">JWT配置</param>
        /// <returns>JWT Token字符串</returns>
        private string GenerateJwtToken(DataRow user, ReZeroJwt jwt)
        {
            var options = SuperAPIModule._apiOptions;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwt.Secret);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user[jwt.UserNameFieldName] + ""));
            foreach (var claim in jwt.Claim ?? new List<Configuration.ClaimItem>())
            {
                claims.Add(new Claim(claim.Key, user[claim.FieldName] + ""));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(jwt?.Expires ?? 1000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
