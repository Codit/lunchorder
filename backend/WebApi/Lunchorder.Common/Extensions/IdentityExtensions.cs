using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Thinktecture.IdentityModel.Tokens;

namespace Lunchorder.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static string GenerateToken(this ClaimsIdentity identity, string audienceId, string symmetricKeyAsBase64, string issuer, DateTimeOffset? issued, DateTimeOffset? expires)
        {
            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var token = new JwtSecurityToken(issuer, audienceId, identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            string jwt = handler.WriteToken(token);
            return jwt;
        }
    }
}
