using System.Collections.Generic;

namespace Lunchorder.Domain.Entities.Configuration
{
    public class EmailInfo
    {
        public string ApiKey { get; set; }
        public string From { get; set; }
        public IEnumerable<string> Bcc { get; set; }
    }
}