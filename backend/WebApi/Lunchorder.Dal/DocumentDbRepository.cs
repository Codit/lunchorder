using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.DocumentDb;
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

        public async Task<GetUserInfoResponse> GetUserInfo(string userId)
        {
            var userQuery = _documentStore.GetItems<User>(o => o.Id == Guid.Parse(userId)).AsDocumentQuery();
            var queryResponse = await userQuery.ExecuteNextAsync<User>();
            var user = queryResponse.FirstOrDefault();
            var userInfo = _mapper.Map<User, GetUserInfoResponse>(user);
            return userInfo;
        }
    }
}