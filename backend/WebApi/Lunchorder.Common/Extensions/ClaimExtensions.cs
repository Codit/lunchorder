using System;
using System.Linq;
using System.Security.Claims;

namespace Lunchorder.Common.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetFullNameFromClaims(this ClaimsIdentity claimsIdentity)
        {
            var firstName = claimsIdentity.Claims.Where(x => x.Type == System.IdentityModel.Claims.ClaimTypes.GivenName).Select(x => x.Value).FirstOrDefault();
            var lastName = claimsIdentity.Claims.Where(x => x.Type == System.IdentityModel.Claims.ClaimTypes.Surname).Select(x => x.Value).FirstOrDefault();

            var fullName = string.Empty;
            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                fullName = $"{firstName} {lastName}";
            }
            return fullName;
        }
    }
}