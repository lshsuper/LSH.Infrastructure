using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2
{

    /// <summary>
    /// JWT-配置
    /// </summary>
    public class JWTSetting
    {

        /// <summary>
        /// 授权方
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 被授权方
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 秘钥
        /// </summary>
        public string SecurityKey { get; set; }
        /// <summary>
        /// 单位:分钟
        /// </summary>
        public int Expires { get; set; }

       


    }

   

}
