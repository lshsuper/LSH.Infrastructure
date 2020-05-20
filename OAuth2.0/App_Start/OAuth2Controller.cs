using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace OAuth2._0.App_Start
{
    public class OAuth2Controller:BaseApiController
    {

        [HttpGet]
        public ApiResult<object> Get()
        {
            
            return Succ<object>(DateTime.Now);
        }

     
    }
}