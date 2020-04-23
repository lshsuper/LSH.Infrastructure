using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api_Demo.Extensions
{
    public class WeChatAuthenticationHandler : IAuthenticationHandler
    {

      
        public AuthenticationScheme _scheme { get; private set; }
       
        protected HttpContext _context { get; private set; }

        /// <summary>
        /// 授权处理
        /// </summary>
        /// <returns></returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            TokenInfo info = new TokenInfo() { 
                        ID=1,
                         Name="lsh"
            };

            var claimsIdentity = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name, info.Name),
                
            }, "DEFAULT_SCHEME_NAME") ;

            AuthenticationTicket ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), _scheme.Name);
           
            return Task.FromResult(AuthenticateResult.Fail(new Exception(ticket.ToString())));
        }


        /// <summary>
        /// 未登录授权
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Task.CompletedTask;
        }


        /// <summary>
        /// 没有具体权限
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }
    }
}
