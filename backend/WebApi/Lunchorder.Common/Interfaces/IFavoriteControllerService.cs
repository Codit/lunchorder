using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IFavoriteControllerService
    {
        Task<IEnumerable<MenuEntryFavorite>> Get(string userId);
        Task Add(string userId, Guid menuEntryId);
        Task Delete(string userId, Guid favoriteId);
    }
}