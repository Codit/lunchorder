using System.Collections.Generic;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Entities.DocumentDb;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class BadgeService
    {
        public List<Badge> Badges;

        public BadgeService()
        {
            Badges = Domain.Constants.Badges.BadgeList;
        }

        public void ExtractBadges(Statistics userStatistics)
        {
             
            // todo, add menuorder here to stats
        }
    }
}