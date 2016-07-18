using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetUserInfoResponse
    {
        public Guid Id { get; set; }
        public double Balance { get; set; }
        public UserProfile Profile { get; set; }
        public IEnumerable<Badge> Badges { get; set; }
        public IEnumerable<MenuEntryFavorite> Favorites { get; set; }
    }
}
