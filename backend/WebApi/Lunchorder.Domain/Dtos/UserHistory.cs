using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a history per user
    /// </summary>
    public class UserHistory
    {
        /// <summary>
        /// An identifier for a user history
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The associated user id
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// A set of order histories for the user
        /// </summary>
        public IEnumerable<UserOrderHistory> OrderHistories { get; set; }
    }
}