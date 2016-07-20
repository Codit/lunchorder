using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;
using Microsoft.AspNet.Identity;
using MenuEntryFavorite = Lunchorder.Domain.Entities.DocumentDb.MenuEntryFavorite;

namespace Lunchorder.Domain.Entities.Authentication
{
    public class ApplicationUser : DocumentDB.AspNet.Identity.IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Picture { get; set; }

        public string Culture { get; set; }

        public int Balance { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }

        public IEnumerable<UserBadge> Badges { get; set; }
        public IEnumerable<MenuEntryFavorite> Favorites { get; set; }
    }
}