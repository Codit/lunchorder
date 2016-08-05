using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    public class LastOrderEntry
    {
        /// <summary>
        /// The order name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The rules that were applied (one per line)
        /// </summary>
        public string AppliedRules { get; set; }

        /// <summary>
        /// The freetext that a user entered
        /// </summary>
        public string FreeText { get; set; }

        /// <summary>
        /// The price for the entry
        /// </summary>
        public decimal Price { get; set; }
    }
}