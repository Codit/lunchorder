using System;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;

namespace Lunchorder.Common.ControllerServices
{
    public class AccountControllerService : IAccountControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IUserService _userService;

        public AccountControllerService(IDatabaseRepository databaseRepository, IUserService userService)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (userService == null) throw new ArgumentNullException(nameof(userService));
            _databaseRepository = databaseRepository;
            _userService = userService;
        }

        public async Task<GetUserInfoResponse> GetUserInfo(string userId, string username, string email)
        {
            // todo check if waad token or not.
            var userInfo = await _databaseRepository.GetUserInfo(userId);

            // if the user is unknown, we store it in our own database.
            if (userInfo == null)
            {
                await _userService.Create(userId, username, email);
                // todo generate token and userinfo
            }

            return userInfo;
        }
    }
}