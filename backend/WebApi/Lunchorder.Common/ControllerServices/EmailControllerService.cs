using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Ploeh.AutoFixture;

namespace Lunchorder.Common.ControllerServices
{
    public class EmailControllerService : IEmailControllerService
    {
        private readonly Fixture _fixture;

        public EmailControllerService()
        {
            _fixture = new Fixture();
        }

        public async Task<bool> SendEmail()
        {
            return await Task.FromResult(_fixture.Create<bool>());
        }
    }
}