using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// The history of the vendor
    /// </summary>
    public class VendorOrderHistory
    {
        /// <summary>
        /// An identifier for the order history at the vendor
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The id of the vendor
        /// </summary>
        public string VendorId { get; set; }

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

        private IEnumerable<VendorOrderHistoryEntry> _entries;
    }
}