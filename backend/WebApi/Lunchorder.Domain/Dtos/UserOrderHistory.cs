using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents order history for a specific user
    /// </summary>
    public class UserOrderHistory
    {
        /// <summary>
        /// An identifier for a user order history
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The final price for a user order history, this represents the price the user actually pays
        /// </summary>
        public int FinalPrice { get; set; }

        /// <summary>
        /// A representation of the entry that the user has ordered
        /// </summary>
        public UserOrderHistoryEntry Entry { get; set; }

        /// <summary>
        /// The time the user has placed the order
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// A set of rules that were applied to the order at that specific time
        /// </summary>
        public IEnumerable<UserOrderHistoryRule> Rules { get; set; }
    }
}