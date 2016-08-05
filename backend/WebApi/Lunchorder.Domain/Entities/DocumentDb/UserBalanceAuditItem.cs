using System;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Represents a balance audit item
    /// </summary>
    public class UserBalanceAuditItem
    {
        public DateTime Date { get; set; }
        public SimpleUser Originator { get; set; }
        public decimal Amount { get; set; }
    }
}