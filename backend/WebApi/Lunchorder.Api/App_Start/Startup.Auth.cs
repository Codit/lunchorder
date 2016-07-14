using Lunchorder.Common.Extensions;
using Owin;

namespace Lunchorder.Api
{
    /// <summary>
    /// Application bootstrap startup partial class
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Starting point for configuring Authentication
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        /// </summary>
        /// <param name="app">An appbuilder instance</param>
        /// <param name="authorizationOptions">The autorization options to use</param>
        public void ConfigureAuth(IAppBuilder app, AuthorizationOptions authorizationOptions)
        {
            app.UseBearerTokenAuthentication(authorizationOptions);
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(authorizationOptions.AzureAdServerOptions);
        }
    }
}