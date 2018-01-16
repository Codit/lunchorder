using System.Collections.Generic;
using Lunchorder.Domain.Constants;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Entities.DocumentDb
{
    public class PushTokenList
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Type => DocumentDbType.PushTokenList;
        public IEnumerable<PushTokenItem> PushTokens { get; set; }
    }
}