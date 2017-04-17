using System.Collections.Generic;
using Newtonsoft.Json;

namespace Lunchorder.Domain.Dtos.Requests
{
    public class RegistrationIdRequest
    {
        [JsonProperty("registration_ids")]
        public IEnumerable<string> RegistrationIds { get; set; }
    }
}