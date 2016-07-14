using System;

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
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the menu category
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the menu category
        /// </summary>
        public string Description { get; set; }
    }
}