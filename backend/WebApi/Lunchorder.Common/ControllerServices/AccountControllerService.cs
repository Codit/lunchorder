using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.DocumentDb;
using Microsoft.Azure.Documents.Linq;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class AccountControllerService : IAccountControllerService
    {
        private readonly IDocumentStore _db;

        public AccountControllerService(IDocumentStore documentStore)
        {
            _db = documentStore;
        }

        public async Task<GetUserInfoResponse> GetUserInfo(string userId)
        {
            var user = _db.GetItems<User>(o => o.Id == Guid.Parse(userId)).AsDocumentQuery();
            var response = await user.ExecuteNextAsync<User>();

            return null;
        }
    }
}