using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Lunchorder.Domain.Entities.Authentication
{
    public class ApplicationUser : DocumentDB.AspNet.Identity.IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Balance { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
}