using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ElCamino.AspNet.Identity.DocumentDB.Model;
using Lunchorder.Domain.Entities.DocumentDb;
using Microsoft.AspNet.Identity;
using MenuEntryFavorite = Lunchorder.Domain.Entities.DocumentDb.MenuEntryFavorite;

namespace Lunchorder.Domain.Entities.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public new string UserId
        {
            get => base.UserId.ToLower();
            set => base.UserId = value;
        }

        public new string Id
        {
            get => base.Id.ToLower();
            set => base.Id = value;
        }

        private IEnumerable<LastOrder> _last5Orders;
        private IEnumerable<UserBalanceAuditItem> _last5BalanceAuditItems;
        private IEnumerable<UserBadge> _badges;
        private IEnumerable<MenuEntryFavorite> _favorites;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Picture { get; set; }

        public string Culture { get; set; }

        public decimal Balance { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }

        public IEnumerable<LastOrder> Last5Orders
        {
            get { return _last5Orders ?? (_last5Orders = new List<LastOrder>()); }
            set { _last5Orders = value; }
        }

        public IEnumerable<UserBalanceAuditItem> Last5BalanceAuditItems
        {
            get { return _last5BalanceAuditItems ?? (_last5BalanceAuditItems = new List<UserBalanceAuditItem>()); }
            set { _last5BalanceAuditItems = value; }
        }

        public IEnumerable<UserBadge> Badges
        {
            get { return _badges ?? (_badges = new List<UserBadge>()); }
            set { _badges = value; }
        }

        public UserBadgeStats UserBadgeStats { get; set; }

        public IEnumerable<MenuEntryFavorite> Favorites
        {
            get { return _favorites ?? (_favorites = new List<MenuEntryFavorite>()); }
            set { _favorites = value; }
        }
    }
}