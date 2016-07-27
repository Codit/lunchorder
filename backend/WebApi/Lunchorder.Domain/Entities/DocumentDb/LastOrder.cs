using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents the last order for a user
    /// </summary>
    public class LastOrder
    {
        private IEnumerable<LastOrderEntry> _lastOrderEntries;

        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Reference to the actual user order
        /// </summary>
        public Guid UserOrderHistoryId { get; set; }

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
        /// The total price of all entries of this order, includes rules
        /// </summary>
        public double FinalPrice { get; set; }
    }
}