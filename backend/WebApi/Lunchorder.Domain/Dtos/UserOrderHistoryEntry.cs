using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// An entry that a user has ordered
    /// </summary>
    public class UserOrderHistoryEntry
    {
        /// <summary>
        /// An identifier for the user order history entry
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The associated menu entry id
        /// </summary>
        public Guid MenuEntryId { get; set; }

        /// <summary>
        /// The name of the entry
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Field where the user can add instructions for the vendor about this order
        /// </summary>
        public string FreeText { get; set; }

        /// <summary>
        /// The price of the menu entry without applying any possible rules
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// A set of rules that were applied to the order at that specific time
        /// </summary>
        public IEnumerable<UserOrderHistoryRule> Rules { get; set; }
    }
}