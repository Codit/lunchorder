namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Details of the menu vendor
    /// </summary>
    public class MenuVendor
    {
        /// <summary>
        /// The id of the menu vendor
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of the vendor
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The address of the menu vendor
        /// </summary>
        public MenuVendorAddress Address { get; set; }

        /// <summary>
        /// The website of the menu vendor
        /// </summary>
        public string Website { get; set; }

        /// <summary>
        /// The ultimate time limit that an order should be submitted to the vendor
        /// </summary>
        public string SubmitOrderTime { get; set; }

        /// <summary>
        /// Logo of the vendor
        /// </summary>
        public string Logo { get; set; }
    }
}