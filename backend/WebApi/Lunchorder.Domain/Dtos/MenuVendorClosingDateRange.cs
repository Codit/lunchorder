namespace Lunchorder.Domain.Dtos
{
    public class MenuVendorClosingDateRange
    {
        /// <summary>
        /// Start date of the closing time (includes this date)
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// End date of the closing time (includes this date)
        /// </summary>
        public string Untill { get; set; }
    }
}