using System;
using System.Collections.Generic;
using Lunchorder.Domain.Constants;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// The history of the vendor
    /// </summary>
    public class VendorOrderHistory
    {
        /// <summary>
        /// An identifier for the order history at the vendor
        /// </summary>
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// The id of the vendor
        /// </summary>
        public Guid VendorId { get; set; }

        /// <summary>
        /// The date for the order in yyyyMMdd
        /// </summary>
        public string OrderDate { get; set; }

        /// <summary>
        /// A set of entries that have been ordered at the vendor
        /// </summary>
        public IEnumerable<VendorOrderHistoryEntry> Entries
        {
            get { return _entries ?? (_entries = new List<VendorOrderHistoryEntry>()); }
            set { _entries = value; }
        }

        /// <summary>
        /// Determines if the order has been sent to the vendor
        /// </summary>
        public bool Submitted { get; set; }

        /// <summary>
        /// Easy query using type
        /// </summary>
        public string Type = DocumentDbType.VendorOrderHistory;

        private IEnumerable<VendorOrderHistoryEntry> _entries;
    }
}