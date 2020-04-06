using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp53
{
    /// <summary>
    /// 微信-提供器
    /// </summary>
    public class WeChatProvider
    {
        private HttpClient _client;
        public WeChatProvider(HttpClient client)
        {
            _client = client;
        }

        #region +API_Method

        #region +About Applet
        /// <summary>
        /// 小程序-授权登录
        /// </summary>
        /// <param name="jsCode"></param>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public async Task<WeChatApplet_JSCode2SessionResult> GetJSCode2Session(string jsCode, string appKey, string appSecret)
        {

            string apiUrl = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
            var msg = await _client.GetAsync(string.Format(apiUrl, appKey, appSecret, jsCode));
            if (msg.StatusCode != HttpStatusCode.OK) return null;

            var data = await msg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeChatApplet_JSCode2SessionResult>(data);
            return result;

        }

        /// <summary>
        ///  小程序-获取Token
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <returns></returns>
        public async Task<WeChatApplet_TokenResult> GetAccess_Token(string appKey, string appSecret)
        {

            string apiUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

            var msg = await _client.GetAsync(string.Format(apiUrl, appKey, appSecret));
            if (msg.StatusCode != HttpStatusCode.OK) return null;

            var data = await msg.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeChatApplet_TokenResult>(data);
            return result;


        }
        /// <summary>
        /// 小程序-获取二维码
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="path"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public async Task<WeChatApple_WXACodeResult> GetWXACode(string access_token, string path, int width)
        {
            string apiUrl = "https://api.weixin.qq.com/wxa/getwxacode?access_token={0}";
            string json = JsonConvert.SerializeObject(new
            {
                path,
                width
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var msg = await _client.PostAsync(string.Format(apiUrl, access_token), content);
            if (msg.StatusCode != HttpStatusCode.OK) return null;

            if (msg.Content.Headers.ContentType.MediaType != "image/jpeg")
            {
                var data = await msg.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<WeChatApple_WXACodeResult>(data);
                return result;
            }

            return new WeChatApple_WXACodeResult()
            {
                byffer = await msg.Content.ReadAsByteArrayAsync(),
                contentType = "image/jpeg",
                errcode = 0,
            };
        } 
        #endregion

        #endregion

        #region +API_Result
        public class WeChatApplet_JSCode2SessionResult
        {
            public string openid { get; set; }

            public string session_key { get; set; }

            public string unionid { get; set; }

            public long errcode { get; set; }
            public string errmsg { get; set; }
        }

        public class WeChatApplet_TokenResult
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }

            public long errcode { get; set; }

            public string errmsg { get; set; }
        }

        public class WeChatApple_WXACodeResult
        {

            public byte[] byffer { get; set; }
            public long errcode { get; set; }

            public string errmsg { get; set; }

            public string contentType { get; set; }
        }
        #endregion

    }

}
