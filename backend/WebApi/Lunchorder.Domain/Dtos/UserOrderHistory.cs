using System;
using System.Collections.Generic;
using System.Linq;

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
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// The name of the user that has placed the entry
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The associated userid for the order
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// A representation of the entr(y)(ies) that the user has ordered
        /// </summary>
        public IEnumerable<UserOrderHistoryEntry> Entries { get; set; }

        /// <summary>
        /// The time the user has placed the order
        /// </summary>
        public DateTime OrderTime { get; set; }
    }
}