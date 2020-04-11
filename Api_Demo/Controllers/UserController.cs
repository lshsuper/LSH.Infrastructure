using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace WebApplication2.Controllers
{

    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]/[action]"), ApiExplorerSettings(GroupName = "User")]

    public class UserController : JWTApiController
    {
        private readonly IOptions<JWTSetting> _jwtSetting;
        public UserController(IOptions<JWTSetting> jwtSetting)
        {
            _jwtSetting = jwtSetting;
        }

        [HttpGet]
        public string Get()
        {
            return "我通过授权啦，i am admin";
        }

        [HttpGet, AllowAnonymous]
        public string Login(string name, string pwd)
        {
            var a = _jwtSetting;
            var claim = new Claim[]{
                   new Claim("UserID",Guid.NewGuid().ToString("N")),
                   new Claim("UserName",name)
                };

            //对称秘钥
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("qwertyuiopasdfghjklzxcvbnm"));
            //签名证书(秘钥，加密算法)
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //生成token  [注意]需要nuget添加Microsoft.AspNetCore.Authentication.JwtBearer包，并引用System.IdentityModel.Tokens.Jwt命名空间
            var token = new JwtSecurityToken("LSH", "HTML", claim, DateTime.Now, DateTime.Now.AddMinutes(30), creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}