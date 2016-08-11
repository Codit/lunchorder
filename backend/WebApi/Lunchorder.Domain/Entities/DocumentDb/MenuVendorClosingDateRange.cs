using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    public class MenuVendorClosingDateRange
    {
        /// <summary>
        /// Start date of the closing time (includes this date)
        /// </summary>
        public DateTime From { get; set; }

        /// <summary>
        /// End date of the closing time (includes this date)
        /// </summary>
        public DateTime Untill { get; set; }
    }
}