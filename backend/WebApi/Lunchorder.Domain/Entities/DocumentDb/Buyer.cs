using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Defines the actual company that will order lunch
    /// </summary>
    public class Buyer
    {
        /// <summary>
        /// An identifier for the buyer
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The name of the buyer
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the buyer
        /// </summary>
        public BuyerAddress Address { get; set; }

        /// <summary>
        /// The valuta that will be used on the website
        /// </summary>
        public string Valuta { get; set; }
    }
}