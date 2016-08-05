using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// An entry that a user has ordered
    /// </summary>
    public class UserOrderHistoryEntry
    {
        /// <summary>
        /// An identifier for the user order history entry
        /// </summary>
        [JsonProperty("id")]
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
        public decimal Price { get; set; }

        /// <summary>
        /// The final price for a user order history, this represents the price the user actually pays
        /// </summary>
        public decimal FinalPrice
        {
            get
            {
                var finalPrice = Price;
                if (Rules == null) return finalPrice;
                finalPrice += Rules.Sum(rule => rule.PriceDelta);
                return finalPrice;
            }
        }

        /// <summary>
        /// A set of rules that were applied to the order at that specific time
        /// </summary>
        public IEnumerable<UserOrderHistoryEntryRule> Rules { get; set; }
    }
}