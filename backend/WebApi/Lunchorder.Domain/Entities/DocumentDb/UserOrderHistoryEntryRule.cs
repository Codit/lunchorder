using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// A rule that has been applied to an user order
    /// </summary>
    public class UserOrderHistoryEntryRule
    {
        /// <summary>
        /// An identifier for an user order history rule
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The description of the user order history rule
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The positive or negative price difference of the user order history rule
        /// </summary>
        public decimal PriceDelta { get; set; }
    }
}