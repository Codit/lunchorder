using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Constants;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.Authentication;
using Microsoft.Azure.Documents.Linq;

namespace Lunchorder.Dal
{
    public class DocumentDbRepository : IDatabaseRepository
    {
        private readonly IDocumentStore _documentStore;
        private readonly IMapper _mapper;

        public DocumentDbRepository(IDocumentStore documentStore, IMapper mapper)
        {
            if (documentStore == null) throw new ArgumentNullException(nameof(documentStore));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));
            _documentStore = documentStore;
            _mapper = mapper;
        }

        public async Task<GetUserInfoResponse> GetUserInfo(string username)
        {
            var userQuery = _documentStore.GetItems<ApplicationUser>(o => o.UserName == username).AsDocumentQuery();
            var queryResponse = await userQuery.ExecuteNextAsync<ApplicationUser>();
            var user = queryResponse.FirstOrDefault();
            var userInfo = _mapper.Map<ApplicationUser, GetUserInfoResponse>(user);

            return userInfo;
        }

        public async Task<IEnumerable<Badge>> GetBadges()
        {
            var badgesQuery = _documentStore.GetItems<Domain.Entities.DocumentDb.BadgeResponse>(x => x.Type == DocumentDbType.Badges).AsDocumentQuery();
            var queryResponse = await badgesQuery.ExecuteNextAsync<Domain.Entities.DocumentDb.BadgeResponse>();

            var badgeResponse = queryResponse.FirstOrDefault();
            if (badgeResponse == null) return null;

            var badges = _mapper.Map<IEnumerable<Domain.Entities.DocumentDb.Badge>, IEnumerable<Badge>>(badgeResponse.Badges);
            return badges;
        }

        public async Task AddMenu(Menu menu)
        {
            var entityMenu = _mapper.Map<Menu, Domain.Entities.DocumentDb.Menu>(menu);
            await _documentStore.UpsertDocumentIfNotExists(menu.Id.ToString(), entityMenu);
        }

        public async Task<Menu> GetEnabledMenu()
        {
            var menu = await GetMenuItem(x => x.Type == DocumentDbType.Menu && x.Enabled && !x.Deleted);
            return _mapper.Map<Domain.Entities.DocumentDb.Menu, Menu>(menu);
        }

        public async Task<Menu> GetMenu(string menuId)
        {
            var menuIdGuid = Guid.Parse(menuId);
            var menu = await GetMenuItem(x => x.Type == DocumentDbType.Menu && !x.Deleted && x.Id == menuIdGuid);
            return _mapper.Map<Domain.Entities.DocumentDb.Menu, Menu>(menu);
        }

        public async Task<Menu> UpdateMenu(Menu menu)
        {
            var entityMenu = _mapper.Map<Menu, Domain.Entities.DocumentDb.Menu>(menu);
            var response = await _documentStore.ReplaceDocument(entityMenu);
            var updatedMenu = (Domain.Entities.DocumentDb.Menu)(dynamic)response.Resource;
            var mappedUpdatedMenu = _mapper.Map<Domain.Entities.DocumentDb.Menu, Menu>(updatedMenu);
            return mappedUpdatedMenu;
        }

        public async Task DeleteMenu(string menuId)
        {
            var menuGuidId = Guid.Parse(menuId);
            var dbMenu = await GetMenuItem(x => x.Id == menuGuidId && !x.Deleted);
            dbMenu.Deleted = true;
            await _documentStore.ReplaceDocument(dbMenu);
        }

        private async Task<Domain.Entities.DocumentDb.Menu> GetMenuItem(Expression<Func<Domain.Entities.DocumentDb.Menu, bool>> predicate)
        {
            var menuQuery = _documentStore.GetItems(predicate).AsDocumentQuery();
            var queryResponse = await menuQuery.ExecuteNextAsync<Domain.Entities.DocumentDb.Menu>();
            var menu = queryResponse.FirstOrDefault();
            return menu;
        }

        public async Task SetActiveMenu(string menuId)
        {
            // disable other menus
            var menuQuery = _documentStore.GetItems<Domain.Entities.DocumentDb.Menu>(x => x.Enabled && x.Type == DocumentDbType.Menu).AsDocumentQuery();
            var enabledMenus = await menuQuery.ExecuteNextAsync<Domain.Entities.DocumentDb.Menu>();
            foreach (var enabledMenu in enabledMenus.ToList())
            {
                enabledMenu.Enabled = false;
                await _documentStore.ReplaceDocument(enabledMenu);
            }

            // enable the requested menu
            var document = await GetMenuItem(x => x.Id == Guid.Parse(menuId));
            document.Enabled = true;

            await _documentStore.ReplaceDocument(document);
        }
    }
}