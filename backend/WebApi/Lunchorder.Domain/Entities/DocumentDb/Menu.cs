using System;
using System.Collections.Generic;
using Lunchorder.Domain.Constants;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Defines the actual menu card for a vendor
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// The identifier for the menu
        /// </summary>
        [JsonProperty("id")]
        public new Guid Id { get; set; }

        /// <summary>
        /// Field that represents the enabled state
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Field that determines if the menu is in a deleted state
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Details of the lunch vendor
        /// </summary>
        public MenuVendor Vendor { get; set; }

        /// <summary>
        /// A set of menu entries that a user could choose from
        /// </summary>
        public IEnumerable<MenuEntry> Entries { get; set; }

        /// <summary>
        /// A set of categories for a Menu Entry
        /// </summary>
        public IEnumerable<MenuCategory> Categories { get; set; }

        /// <summary>
        /// A set of rules that can be applied to one or more MenuCategories 
        /// </summary>
        public IEnumerable<MenuRule> Rules { get; set; }

        /// <summary>
        /// Last change of the menu
        /// </summary>
        public DateTime LastUpdated { get; set; }

        /// <summary>
        /// How many times has it been updated
        /// </summary>
        public int Revision { get; set; }
        
        /// <summary>
        /// Easy query
        /// </summary>
        public string Type = DocumentDbType.Menu;
    }
}