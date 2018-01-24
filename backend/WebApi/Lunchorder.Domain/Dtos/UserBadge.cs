using System;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Badge earned by user that has a reference to the actual badge detail
    /// </summary>
    public class UserBadge
    {
        /// <summary>
        /// Reference to the actual badge
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Times the badge has been assigned to the user
        /// </summary>
        public int TimesEarned { get; set; }
    }
}