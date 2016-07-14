using Microsoft.Owin.Security.ActiveDirectory;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;

namespace Lunchorder.Common.Extensions
{
    /// <summary>
    /// Set of security authorization option properties
    /// </summary>
    public class AuthorizationOptions
    {
        /// <summary>
        /// Server options for OAuth Authorization
        /// </summary>
        public OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions { get; set; }

        /// <summary>
        /// Options for JwtBearer Authentication
        /// </summary>
        public JwtBearerAuthenticationOptions JwtBearerAuthenticationOptions { get; set; }

        public WindowsAzureActiveDirectoryBearerAuthenticationOptions AzureAdServerOptions { get; set; }
    }
}