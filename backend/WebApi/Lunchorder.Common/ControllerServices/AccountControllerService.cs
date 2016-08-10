using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Lunchorder.Common.Extensions;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos.Responses;
using Microsoft.AspNet.Identity;

namespace Lunchorder.Common.ControllerServices
{
    public class AccountControllerService : IAccountControllerService
    {
        private readonly IDatabaseRepository _databaseRepository;
        private readonly IUserService _userService;
        private readonly IConfigurationService _configurationService;

        public AccountControllerService(IDatabaseRepository databaseRepository, IUserService userService, IConfigurationService configurationService)
        {
            if (databaseRepository == null) throw new ArgumentNullException(nameof(databaseRepository));
            if (userService == null) throw new ArgumentNullException(nameof(userService));
            if (configurationService == null) throw new ArgumentNullException(nameof(configurationService));
            _databaseRepository = databaseRepository;
            _userService = userService;
            _configurationService = configurationService;
        }

        public async Task<GetUserInfoResponse> GetUserInfo(ClaimsIdentity claimsIdentity)
        {
            bool isAzureActiveDirectoryUser = false;
            var hasIssClaim = claimsIdentity.FindFirst("iss");
            if (hasIssClaim != null)
            {
                if (hasIssClaim.Value.Contains("sts.windows.net"))
                {
                    isAzureActiveDirectoryUser = true;
                }
            }
            
            var userName = claimsIdentity.GetUserName();
            
            var userInfo = await _databaseRepository.GetUserInfo(userName);
            
            // if the user is unknown, we store it in our own database.
            if (userInfo == null && isAzureActiveDirectoryUser)
            {
                var user = await _userService.Create(userName, userName);
                await _databaseRepository.AddToUserList(user.Id, user.UserName, user.FirstName, user.LastName);
                userInfo = await _databaseRepository.GetUserInfo(userName);
            }

            var roleClaims = new List<Claim>();
            foreach (var userRole in userInfo.Roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var localClaimsIdentity = new ClaimsIdentity(
                   new List<Claim>
                   {
                    new Claim(ClaimTypes.Name, userInfo.UserName),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString())
                   });
            localClaimsIdentity.AddClaims(roleClaims);

            var token = localClaimsIdentity.GenerateToken(_configurationService.LocalAuthentication.AudienceId, _configurationService.LocalAuthentication.AudienceSecret, _configurationService.LocalAuthentication.Issuer, DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddDays(10));
            userInfo.UserToken = token;
            return userInfo;
        }

        public async Task<GetAllUsersResponse> GetAllUsers()
        {
            var users = await _databaseRepository.GetUsers();
            return new GetAllUsersResponse() { Users = users };
        }
    }
}