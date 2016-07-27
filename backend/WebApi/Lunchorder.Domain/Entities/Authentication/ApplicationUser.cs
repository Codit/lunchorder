using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Domain.Entities.DocumentDb;
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

        public double Balance { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }

        public IEnumerable<LastOrder> Last5Orders { get; set; }
        public IEnumerable<UserBadge> Badges { get; set; }
        public UserBadgeStats UserBadgeStats { get; set; } 
        public IEnumerable<MenuEntryFavorite> Favorites { get; set; }
    }
}