using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth2._0.App_Start
{
    public class JsonHelper
    {


        private readonly static JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss",
        };
        public static JsonSerializerSettings GetSetting()
        {

            return _settings;

        }

    }
}