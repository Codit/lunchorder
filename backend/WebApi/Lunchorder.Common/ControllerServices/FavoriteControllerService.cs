using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class FavoriteControllerService : IFavoriteControllerService
    {
        private readonly Fixture _fixture;

        public FavoriteControllerService()
        {
            _fixture = new Fixture();
        }

        public Task<IEnumerable<MenuEntryFavorite>> Get(string empty)
        {
            return Task.FromResult(_fixture.Create<IEnumerable<MenuEntryFavorite>>());
        }

        public Task Add(string userId, Guid menuEntryId)
        {
            return null;
        }

        public Task Delete(string userId, Guid favoriteId)
        {

            return null;
        }
    }
}