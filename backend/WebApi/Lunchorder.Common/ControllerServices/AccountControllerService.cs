using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class AccountControllerService : IAccountControllerService
    {
        private readonly Fixture _fixture;

        public AccountControllerService()
        {
            _fixture = new Fixture();
        }

        public async Task<GetUserInfoResponse> GetUserInfo(string userId)
        {
            return await Task.FromResult(_fixture.Create<GetUserInfoResponse>());
        }
    }
}