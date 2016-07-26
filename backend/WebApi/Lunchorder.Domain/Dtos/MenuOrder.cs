using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// A specific menu order for a user
    /// </summary>
    public class MenuOrder
    {
        /// <summary>
        /// The associated menu entry id
        /// </summary>
        public Guid MenuEntryId { get; set; }

        /// <summary>
        /// The name of the menu item at that time
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Freetext that a user can choose
        /// </summary>
        public string FreeText { get; set; }

        /// <summary>
        /// The checked rules that are applied to the associated menu entry
        /// </summary>
        public IEnumerable<MenuRule> AppliedMenuRules { get; set; }

        /// <summary>
        /// The price for the order
        /// </summary>
        public double Price;
    }
}