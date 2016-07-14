using System;

namespace Lunchorder.Domain.Dtos.Requests
{
    public class PostFavoriteRequest
    {
        public Guid MenuEntryId { get; set; }
    }
}
