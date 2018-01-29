using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetBadgesResponse
    {
        public IEnumerable<Badge> Badges { get; set; }

        /// <summary>
        /// Top badge rank (winner first)
        /// </summary>
        public IEnumerable<BadgeRanking> BadgeRankings { get; set; }
    }
}