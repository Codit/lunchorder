using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetUserInfoResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public double Balance { get; set; }
        public UserProfile Profile { get; set; }
        public IEnumerable<UserBadge> Badges { get; set; }
        public IEnumerable<MenuEntryFavorite> Favorites { get; set; }

        /// <summary>
        /// A new token for the user in case of user creation in our database.
        /// </summary>
        public string UserToken { get; set; }
    }
}
