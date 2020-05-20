using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSH.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize(AuthenticationSchemes = "DEFAULT_SCHEME_NAME")]
    public class WeChatController : ControllerBase
    {
        public  UserService _userService { get; set; }
       
        [HttpGet]
        public  string Get()
        {

            try
            {
                throw new NotImplementedException("未实现");
            }
            catch (Exception ex)
            {

                LogHelper.Error("lsh-error",ex);
            }
           // LogHelper.Debug("lsh");
        
            return null;
           // HttpContext.Authentication.SignInAsync("lsh",);
           
        }

    }
}