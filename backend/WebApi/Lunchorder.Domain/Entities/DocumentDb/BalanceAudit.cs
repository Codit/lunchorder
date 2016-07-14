using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Keeps track of changes to balance for users
    /// </summary>
    public class BalanceAudit
    {
        /// <summary>
        /// An identifier for the balance audit
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Time there was a modification to the users balance
        /// </summary>
        public DateTime AuditDateTime { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Amount of balance change
        /// </summary>
        public int Amount { get; set; }
    }
}