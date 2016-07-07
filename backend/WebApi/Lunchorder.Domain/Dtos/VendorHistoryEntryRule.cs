using System;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a set of rules that were applied for an entry at a point in time
    /// </summary>
    public class VendorHistoryEntryRule
    {
        /// <summary>
        /// An identifier for the vendor history entry rule
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The description of the rule
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The positive or negative price difference of the vendor history entry rule
        /// </summary>
        public int PriceDelta { get; set; }
    }
}