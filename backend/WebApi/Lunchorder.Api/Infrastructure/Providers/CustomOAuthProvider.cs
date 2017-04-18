using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.Authentication;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Lunchorder.Api.Infrastructure.Providers
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly Func<UserManager<ApplicationUser>> _userManager;
        private static IMapper _mapper;

        public CustomOAuthProvider(Func<UserManager<ApplicationUser>> userManager, IMapper mapper)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager;
            _mapper = mapper;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            UserManager<ApplicationUser> localUserManager;
            localUserManager = _userManager();

            ApplicationUser user = await localUserManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("Login failed", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(localUserManager, "JWT");

            AuthenticationProperties properties = CreateProperties(user);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);

            context.Validated(ticket);

        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Creates properties to add to the token response
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(ApplicationUser user)
        {
            var getUserInfoResponse = _mapper.Map<ApplicationUser, GetUserInfoResponse>(user);

            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "payload",  getUserInfoResponse.ToJson() }
            };
            return new AuthenticationProperties(data);
        }
    }
}