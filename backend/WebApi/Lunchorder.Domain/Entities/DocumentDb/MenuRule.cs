using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// A rule for a menu category
    /// </summary>
    public class MenuRule
    {
        /// <summary>
        /// An identifier for a menu rule
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// A set of the associated categories
        /// </summary>
        public IEnumerable<Guid> CategoryIds { get; set; }

        /// <summary>
        /// The rule description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The positive or negative price difference
        /// </summary>
        public int PriceDelta { get; set; }

        /// <summary>
        /// Field that represents the enabled state
        /// </summary>
        public bool Enabled { get; set; }
    }
}