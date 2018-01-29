namespace Lunchorder.Domain.Dtos
{
    public class Statistics
    {
        /// <summary>
        /// The total amount of money spent on the application
        /// </summary>
        public decimal AppTotalSpend { get; set; }

        /// <summary>
        /// The total amount spent for this week
        /// </summary>
        public decimal WeeklyTotalAmount { get; set; }

        /// <summary>
        /// The total amount spent for this month
        /// </summary>
        public decimal MonthlyTotalAmount { get; set; }

        /// <summary>
        /// The number of healthy orders this week
        /// </summary>
        public int WeeklyHealthyOrders { get; set; }

        /// <summary>
        /// The total amount spent for this year
        /// </summary>
        public decimal YearlyTotalAmount { get; set; }

        /// <summary>
        /// The number of pastas ordered for a year
        /// </summary>
        public int YearlyPastas { get; set; }

        /// <summary>
        /// The total amount of prepayed money
        /// </summary>
        public decimal PrepayedTotal { get; set; }
    }  
}