using System.Collections.Generic;
using Lunchorder.Domain.Constants;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    /// <summary>
    /// Keeps track of changes to balance for users
    /// </summary>
    public class UserBalanceAudit
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Type => DocumentDbType.UserBalanceAudit;
        public IEnumerable<UserBalanceAuditItem> Audits { get; set; }
    }
}