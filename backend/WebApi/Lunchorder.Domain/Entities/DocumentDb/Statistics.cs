using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Holds specific statistics for a user to easy show info and do badge calculation.
    /// </summary>
    public class Statistics
    {
        private List<DayTotal> _dayTotals;
        private List<WeeklyTotal> _weeklyTotals;
        private List<MonthlyTotal> _monthlyTotals;
        private List<YearlyTotal> _yearlyTotals;

        /// <summary>
        /// The total amount of money spent on the application
        /// </summary>
        public decimal AppTotalSpend { get; set; }

        /// <summary>
        /// The number of orders per day
        /// </summary>
        public List<DayTotal> DayTotals
        {
            get => _dayTotals ?? (_dayTotals = new List<DayTotal>());
            set => _dayTotals = value;
        }

        /// <summary>
        /// The total orders per week
        /// </summary>
        public List<WeeklyTotal> WeeklyTotals
        {
            get => _weeklyTotals ?? (_weeklyTotals = new List<WeeklyTotal>());
            set => _weeklyTotals = value;
        }

        /// <summary>
        /// The total orders per month
        /// </summary>
        public List<MonthlyTotal> MonthlyTotals
        {
            get => _monthlyTotals ?? (_monthlyTotals = new List<MonthlyTotal>());
            set => _monthlyTotals = value;
        }

        public int WeeklyHealthyOrders
        {
            get
            {
                var weekTotal = WeeklyTotals.FirstOrDefault(x => x.WeekDate == x.CurrentWeekDate());
                if (weekTotal != null) { return weekTotal.HealthyOrderCount; }
                return 0;
            }
        }

        /// <summary>
        /// The total orders per year
        /// </summary>
        public List<YearlyTotal> YearlyTotals
        {
            get => _yearlyTotals ?? (_yearlyTotals = new List<YearlyTotal>());
            set => _yearlyTotals = value;
        }

        /// <summary>
        /// The number of pastas ordered for a year
        /// </summary>
        public int YearlyPastas
        {
            get
            {
                var yearTotal = YearlyTotals.FirstOrDefault(x => x.YearDate == DateTime.UtcNow.Year);
                return yearTotal?.PastaOrderCount ?? 0;
            }
        }

        /// <summary>
        /// The total amount of prepayed money
        /// </summary>
        public decimal PrepayedTotal { get; set; }
    }

    public class DayTotal
    {
        public DayTotal()
        {
            DayDate = CurrentDayDate();
        }

        public DayTotal(DateTime dateTime)
        {
            DayDate = ParseDay(dateTime);
        }

        /// <summary>
        /// The current week in YYYYDAYOFYEAR format
        /// </summary>
        public string DayDate { get; set; }

        /// <summary>
        /// The times an order has been made
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// The total amount spent for today
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Helper method to construct the correct <see cref="DayDate"/>
        /// </summary>
        /// <param name="dateTime">A datetime to get the parse the DayDate format</param>
        /// <returns></returns>
        public string ParseDay(DateTime dateTime)
        {
            return $"{dateTime.Year}{dateTime.DayOfYear}";
        }

        public string CurrentDayDate()
        {
            return ParseDay(DateTime.UtcNow);
        }
    }

    public class WeeklyTotal
    {
        /// <summary>
        /// Default constructor for current week
        /// </summary>
        public WeeklyTotal()
        {
            WeekDate = CurrentWeekDate();
        }

        public WeeklyTotal(DateTime dateTime)
        {
            WeekDate = ParseWeek(dateTime);
        }

        /// <summary>
        /// The current week in YYYWEEKOFYEAR format
        /// </summary>
        public string WeekDate { get; set; }

        /// <summary>
        /// The times an order has been made
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// The total amount spent for this week
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Keeps track if the High Roller Badge has been earned yet
        /// </summary>
        public bool HasHighRollerBadge { get; set; }

        /// <summary>
        /// Keeps track if the Healthy Badge has been earned yet
        /// </summary>
        public bool HasHealthyBadge { get; set; }

        /// <summary>
        /// The total healthy orders for this week
        /// </summary>
        public int HealthyOrderCount { get; set; }

        /// <summary>
        /// Helper method to construct the correct <see cref="WeekDate"/>
        /// </summary>
        /// <param name="dateTime">A datetime to get the parse the WeekDate format</param>
        /// <returns></returns>
        public string ParseWeek(DateTime dateTime)
        {
            return $"{dateTime.Year}{GetWeekOfYear(dateTime)}";
        }

        public string CurrentWeekDate()
        {
            return ParseWeek(DateTime.UtcNow);
        }

        public int GetWeekOfYear(DateTime date)
        {
            var dateTimeFormatInfo = DateTimeFormatInfo.InvariantInfo;
            var calendar = dateTimeFormatInfo.Calendar;
            var weekOfYear = calendar.GetWeekOfYear(date, dateTimeFormatInfo.CalendarWeekRule, dateTimeFormatInfo.FirstDayOfWeek);

            return weekOfYear;
        }
    }

    public class MonthlyTotal
    {
        /// <summary>
        /// Default constructor for current month
        /// </summary>
        public MonthlyTotal()
        {
            MonthDate = CurrentMonthDate();
        }

        public MonthlyTotal(DateTime dateTime)
        {
            MonthDate = ParseMonth(dateTime);
        }

        /// <summary>
        /// The current month in YYYYMM format
        /// </summary>
        public string MonthDate { get; set; }

        /// <summary>
        /// The times an order has been made
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// Keeps track if the Deep Pockets Badge has been earned yet
        /// </summary>
        public bool HasDeepPocketsBadge { get; set; }

        /// <summary>
        /// The total amount spent for this month
        /// </summary>
        public decimal Amount { get; set; }

        public string CurrentMonthDate()
        {
            return ParseMonth(DateTime.UtcNow);
        }

        /// <summary>
        /// Helper method to construct the correct <see cref="MonthDate"/>
        /// </summary>
        /// <param name="dateTime">A datetime to get the parse the WeekDate format</param>
        /// <returns></returns>
        public string ParseMonth(DateTime dateTime)
        {
            return $"{dateTime.Year}{dateTime.Month}";
        }
    }

    public class YearlyTotal
    {
        /// <summary>
        /// Default constructor for current year
        /// </summary>
        public YearlyTotal()
        {
            YearDate = DateTime.UtcNow.Year;
        }

        public YearlyTotal(DateTime dateTime)
        {
            YearDate = dateTime.Year;
        }

        /// <summary>
        /// The current month in YYYY format
        /// </summary>
        public int YearDate { get; set; }

        /// <summary>
        /// The times an order has been made
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// The times a pasta order has been made
        /// </summary>
        public int PastaOrderCount { get; set; }

        /// <summary>
        /// The total amount spent for this year
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Helper method to construct the correct <see cref="YearDate"/>
        /// </summary>
        /// <param name="dateTime">A datetime to get the parse the WeekDate format</param>
        /// <returns></returns>
        public int ParseYear(DateTime dateTime)
        {
            return dateTime.Year;
        }
    }
}