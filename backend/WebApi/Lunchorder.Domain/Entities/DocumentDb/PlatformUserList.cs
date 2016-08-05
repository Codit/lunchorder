using System.Collections.Generic;
using Lunchorder.Domain.Constants;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    public class PlatformUserList
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Type => DocumentDbType.PlatformUserList;
        public IEnumerable<PlatformUserListItem> Users { get; set; }
    }
}