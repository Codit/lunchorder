using System;
using System.Collections.Generic;
using Lunchorder.Domain.Constants;

namespace Lunchorder.Domain.Entities.DocumentDb
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
        /// The associated userid for the order
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The final price for a user order history, this represents the price the user actually pays
        /// </summary>
        public double FinalPrice { get; set; }

        /// <summary>
        /// A representation of the entr(y)(ies) that the user has ordered
        /// </summary>
        public IEnumerable<UserOrderHistoryEntry> Entries { get; set; }

        /// <summary>
        /// The time the user has placed the order
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// A set of rules that were applied to the order at that specific time
        /// </summary>
        public IEnumerable<UserOrderHistoryRule> Rules { get; set; }

        /// <summary>
        /// Easy query on type
        /// </summary>
        public string Type = DocumentDbType.UserHistory;
    }
}