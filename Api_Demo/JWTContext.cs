using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2
{

    /// <summary>
    /// JWT-Context
    /// </summary>
    public class JWTContext
    {
        /// <summary>
        ///获取JWT-Token
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static string GetToken(Claim[] claims, JWTSetting setting)
        {
            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.SecurityKey));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var token = new JwtSecurityToken(setting.Issuer, setting.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(setting.Expires), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
