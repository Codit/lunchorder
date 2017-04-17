using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a reminder for a user
    /// </summary>
    public class Reminder
    {
        public ReminderType Type { get; set; }

        /// <summary>
        /// Number of minutes before a final order that the reminder should execute
        /// </summary>
        public int Minutes { get; set; }

        public string Action { get; set; }
    }
}