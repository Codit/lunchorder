using System;

namespace Lunchorder.Common
{
    public class DateGenerator
    {
        /// <summary>
        /// Generates the today date in yyyyMMdd format
        /// </summary>
        /// <returns>Date string in yyyyMMdd format</returns>
        public virtual string GenerateDateFormat(DateTime date)
        {
            return date.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
