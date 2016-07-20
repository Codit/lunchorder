using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lunchorder.Common.Interfaces;
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
            var queryResponse = userQuery.ExecuteNextAsync<ApplicationUser>();

            // await bug in document db? http://stackoverflow.com/questions/27083501/documentdb-call-hangs
            queryResponse.Wait();
            var user = queryResponse.Result.FirstOrDefault();
            var userInfo = _mapper.Map<ApplicationUser, GetUserInfoResponse>(user);

            return userInfo;
        }
    }
}