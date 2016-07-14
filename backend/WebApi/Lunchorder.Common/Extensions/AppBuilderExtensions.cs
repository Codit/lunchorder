using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Lunchorder.Common.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class AppBuilderExtensions
    {
        /// <summary>
        /// Configures bearer token authentication
        /// </summary>
        /// <param name="appBuilder">An appbuilder instance</param>
        /// <param name="authorizationOptions">The AuthorizationOptions to use</param>
        public static void UseBearerTokenAuthentication(this IAppBuilder appBuilder, AuthorizationOptions authorizationOptions)
        {
            ConfigureOAuthGeneration(appBuilder, authorizationOptions.OAuthAuthorizationServerOptions);
            ConfigureOAuthConsumption(appBuilder, authorizationOptions.JwtBearerAuthenticationOptions);
        }

        private static void ConfigureOAuthGeneration(IAppBuilder app, OAuthAuthorizationServerOptions oAuthServerOptions)
        {
            //// OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(oAuthServerOptions);
        }

        private static void ConfigureOAuthConsumption(IAppBuilder app, JwtBearerAuthenticationOptions jwtBearerAuthenticationOptions)
        {
            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(jwtBearerAuthenticationOptions);
        }
    }
}