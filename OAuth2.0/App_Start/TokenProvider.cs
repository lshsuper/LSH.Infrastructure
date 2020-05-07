using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OAuth2._0.App_Start
{
    public class TokenProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            return base.ValidateClientAuthentication(context);
        }
        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {

            return base.GrantAuthorizationCode(context);    
        }
    }
}