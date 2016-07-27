using System;
using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// A menu category
    /// </summary>
    public class MenuCategory
    {
        /// <summary>
        /// An identifier for the menu category
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the menu category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the menu category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Possible subcategories
        /// </summary>
        public IEnumerable<MenuCategory> SubCategories { get; set; }
    }
}