using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a reminder for a user
    /// </summary>
    public class Reminder
    {
        public ReminderType ReminderType { get; set; }
        public DateTime ReminderDate { get; set; }
    }
}