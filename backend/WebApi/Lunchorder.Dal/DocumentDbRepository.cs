using System;
using System.Collections.Generic;
using System.Linq;
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
            var badgesQuery = _documentStore.GetItems<Domain.Entities.DocumentDb.BadgeResponse>(x => x.Type == DocumentDbTypes.Badges).AsDocumentQuery();
            var queryResponse = await badgesQuery.ExecuteNextAsync<Domain.Entities.DocumentDb.BadgeResponse>();

            var badgeResponse = queryResponse.FirstOrDefault();
            if (badgeResponse == null) return null;

            var badges =
                _mapper.Map<IEnumerable<Domain.Entities.DocumentDb.Badge>, IEnumerable<Badge>>(
                    badgeResponse.Badges);
            return badges;
        }
    }
}