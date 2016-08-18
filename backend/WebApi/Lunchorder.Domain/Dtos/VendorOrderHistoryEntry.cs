using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents an entry that has been ordered at the vendor
    /// </summary>
    public class VendorOrderHistoryEntry
    {
        private string _fullName;

        /// <summary>
        /// An identifier for the vendor entry history
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the entry
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The associated menu entry id
        /// </summary>
        public string MenuEntryId { get; set; }

        /// <summary>
        /// The id of the user that has placed the entry
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// The name of the user that has placed the entry
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The full name of the user that has placed the entry
        /// </summary>
        public string FullName
        {
            get { return string.IsNullOrEmpty(_fullName) ?  UserName : _fullName; }
            set { _fullName = value; }
        }

        /// <summary>
        /// The price that has been paid by the user to order this entry at the vendor
        /// </summary>
        public decimal FinalPrice { get; set; }

        /// <summary>
        /// The associated user order history
        /// </summary>
        public string UserOrderHistoryEntryId { get; set; }

        /// <summary>
        /// A set of vendor entry rules that were applied at that time
        /// </summary>
        public IEnumerable<VendorHistoryEntryRule> Rules { get; set; }
    }
}