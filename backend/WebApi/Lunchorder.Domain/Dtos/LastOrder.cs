using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents the last order for a user
    /// </summary>
    public class LastOrder
    {
        private IEnumerable<LastOrderEntry> _lastOrderEntries;

        public string Id { get; set; }

        /// <summary>
        /// Reference to the actual user order
        /// </summary>
        public string UserOrderHistoryId { get; set; }

        /// <summary>
        /// The time when the order was submitted
        /// </summary>
        public DateTime OrderTime { get; set; }

        /// <summary>
        /// The items associated with the order
        /// </summary>
        public IEnumerable<LastOrderEntry> LastOrderEntries
        {
            get { return _lastOrderEntries ?? (_lastOrderEntries = new List<LastOrderEntry>()); }
            set { _lastOrderEntries = value; }
        }

        /// <summary>
        /// The total price of all entries of this order including rules
        /// </summary>
        public decimal FinalPrice { get; set; }
    }
}