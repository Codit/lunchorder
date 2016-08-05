using System.Collections.Generic;

namespace Lunchorder.Domain.Dtos.Responses
{
    public class GetAllUsersResponse
    {
        public IEnumerable<PlatformUserListItem> Users { get; set; }
    }
}