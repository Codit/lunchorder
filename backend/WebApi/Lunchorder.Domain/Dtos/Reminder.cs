namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a reminder for a user
    /// </summary>
    public class Reminder
    {
        public int Type { get; set; }

        /// <summary>
        /// Number of minutes before a final order that the reminder should execute
        /// </summary>
        public int Minutes { get; set; }
    }
}