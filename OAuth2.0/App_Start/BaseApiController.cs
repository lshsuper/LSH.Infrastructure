using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;

namespace OAuth2._0.App_Start
{
    public class BaseApiController : ApiController
    {
       
        public ApiResult<T> Fail<T>(T data,string error = null)
        {
            return new ApiResult<T>()
            {
                Error = error,
                Data = data,
                Status = false
            };
        }

        public ApiResult<T> Succ<T>(T data) {

            return new ApiResult<T>()
            {
                Error =string.Empty,
                Data = data,
                Status =true
            };
        }


    }


    public class ApiResult<T>
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("data")]
        public T Data { get; set; }
        [JsonProperty("status")]
        public bool Status { get; set; }




    }
}