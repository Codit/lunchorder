using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Details of the menu vendor
    /// </summary>
    public class MenuVendor
    {
        /// <summary>
        /// The address of the menu vendor
        /// </summary>
        public MenuVendorAddress Address { get; set; }

        /// <summary>
        /// The ultimate time limit that an order should be submitted to the vendor
        /// </summary>
        public TimeSpan SubmitOrderTime { get; set; }
    }
}