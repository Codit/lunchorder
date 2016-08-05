using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// A menu entry
    /// </summary>
    public class MenuEntry
    {
        /// <summary>
        /// An identifier for the menu entry
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the menu entry
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the menu entry
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The associated category for the menu entry
        /// </summary>
        public Guid CategoryId { get; set; }

        /// <summary>
        /// An URL that contains a picture for the menu entry
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// The price for the menu entry
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Represents if the entry is in an enabled state
        /// </summary>
        public bool Enabled { get; set; }
    }
}