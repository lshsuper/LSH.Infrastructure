using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using OAuth2._0.App_Start;
using Owin;

[assembly: OwinStartup(typeof(OAuth2._0.Startup))]

namespace OAuth2._0
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888

            OAuthAuthorizationServerOptions opt = new OAuthAuthorizationServerOptions()
            {

                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/access_token"),
                AuthorizeEndpointPath = new PathString("/authorize"),
                Provider = new TokenProvider(),



            };
            app.UseOAuthBearerTokens(opt);

            app.UseWebApi();

        }
    }
}
