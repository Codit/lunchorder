using System;

namespace Lunchorder.Domain.Dtos
{
    /// <summary>
    /// Represents a balance audit item
    /// </summary>
    public class UserBalanceAuditItem
    {
        public DateTime Date { get; set; }
        public string OriginatorName { get; set; }
        public decimal Amount { get; set; }
    }
}