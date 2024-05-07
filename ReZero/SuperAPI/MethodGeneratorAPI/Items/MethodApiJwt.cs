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
        public string GetToken(string userName, string password) 
        {
            var db = App.Db;
            var options = SuperAPIModule._apiOptions;
            var jwt=options?.InterfaceOptions?.Jwt??new Configuration.ReZeroJwt();
             
            if (string.IsNullOrEmpty(jwt.Secret)||string.IsNullOrEmpty(jwt.UserTableName) || string.IsNullOrEmpty(jwt.UserTableName) || string.IsNullOrEmpty(jwt.UserTableName))
            {
                throw new Exception(TextHandler.GetCommonText("请到json文件配置jwt信息", "Go to the json file to configure the jwt information"));
            } 
            var dt = db.Queryable<object>()
                .AS(jwt.UserTableName)
                .Where(jwt.PasswordFieldName, "=", password)
                .Where(jwt.UserNameFieldName, "=", userName)
                .ToDataTable();

            if (dt.Rows.Count == 0) 
            {
                throw new Exception(TextHandler.GetCommonText("授权失败", "Authorization failure"));
            }
           return GenerateJwtToken(dt.Rows[0],jwt);
        } 
        private string GenerateJwtToken(DataRow user,ReZeroJwt jwt)
        {
            var options= SuperAPIModule._apiOptions;
            var tokenHandler = new JwtSecurityTokenHandler(); 
            var key = Encoding.ASCII.GetBytes(jwt.Secret);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user[jwt.UserNameFieldName]+""));
            foreach (var claim in jwt.Claim??new List<Configuration.ClaimItem>() ) 
            {
                claims.Add(new Claim(claim.Key, user[claim.FieldName]+""));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddMinutes(jwt?.Expires??1000),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
