using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{

    /// <summary>
    /// 基础数据
    /// </summary>

    [Route("api/[controller]/[action]"),ApiExplorerSettings(GroupName ="Basic")]
    public class HomeController:JWTApiController
    {
        [HttpGet]
        public string Get()
        {
            return "基础";
        }
    }
}
