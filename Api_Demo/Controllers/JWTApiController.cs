using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// 基类控制器
    /// </summary>

    [ApiController, Authorize]
    public class JWTApiController :Controller
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UserID { get; private set; }
        /// <summary>
        /// 用户授权信息
        /// </summary>
        public Passport Passport { get; private set; }
        /// <summary>
        /// 控制器执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            UserID = User.Claims.FirstOrDefault(f => f.Type == JWTClaimTypeConst.UserID)?.Value;
            string userName = User.Claims.FirstOrDefault(f => f.Type == JWTClaimTypeConst.UserName)?.Value,
                   headImage= User.Claims.FirstOrDefault(f => f.Type == JWTClaimTypeConst.HeadImage)?.Value;
           
            //此处可从redis获取具体信息
            Passport = new Passport()
            {
                UserID = UserID,
                UserName = userName,
                HeadImage= headImage
            };
            base.OnActionExecuting(context);
        }


    }
}