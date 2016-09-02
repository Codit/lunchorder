using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos
{
    public class UserBalanceAudit
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
        public IEnumerable<UserBalanceAuditItem> Audits { get; set; }
    }
}