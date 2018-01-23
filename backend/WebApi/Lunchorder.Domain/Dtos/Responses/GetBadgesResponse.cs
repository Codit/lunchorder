using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetBadgesResponse
    {
        public IEnumerable<Badge> Badges { get; set; }
    }
}