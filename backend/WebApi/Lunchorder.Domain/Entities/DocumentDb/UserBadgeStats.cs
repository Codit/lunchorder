using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Holds badge specific statistics for a user to easy calculate badges.
    /// </summary>
    public class UserBadgeStats
    {
        // todo, this is just some brainstorming, needs detailed implementation.
        public int TotalOrders { get; set; }
        public double TotalSpent { get; set; }
        public DateTime LastOrderTime { get; set; }
        public double LastPrepayed { get; set; }
        public int HealthyItems { get; set; }
        public int PastaItems { get; set; }
        public int SurpriseItems { get; set; }
        public int ItemsAtOnce { get; set; }
    }
}