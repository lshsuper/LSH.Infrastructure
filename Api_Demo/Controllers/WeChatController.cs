using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "DEFAULT_SCHEME_NAME")]
    public class WeChatController : ControllerBase
    {

        [HttpGet]
        public  string Get()
        {
           // HttpContext.Authentication.SignInAsync("lsh",);
            return "ok";
        }

    }
}