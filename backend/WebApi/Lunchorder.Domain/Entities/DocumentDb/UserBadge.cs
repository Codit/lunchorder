using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Badge earned by user that has a reference to the actual badge detail
    /// </summary>
    public class UserBadge
    {
        /// <summary>
        /// Reference to the actual badge
        /// </summary>
        public Guid BadgeId { get; set; }

        /// <summary>
        /// Times the badge has been assigned to the user
        /// </summary>
        public int TimesEarned { get; set; }
    }
}