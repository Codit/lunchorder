using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Lunchorder.Common.Extensions;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Dtos;
using Lunchorder.Domain.Dtos.Responses;
using Lunchorder.Domain.Entities.Authentication;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

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
                var firstName = claimsIdentity.Claims.Where(x => x.Type == System.IdentityModel.Claims.ClaimTypes.GivenName).Select(x => x.Value).FirstOrDefault();
                var lastName = claimsIdentity.Claims.Where(x => x.Type == System.IdentityModel.Claims.ClaimTypes.Surname).Select(x => x.Value).FirstOrDefault();

                var user = await _userService.Create(userName, userName, firstName, lastName);
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
                    new Claim(ClaimTypes.GivenName, userInfo.Profile.FirstName),
                    new Claim(ClaimTypes.Surname, userInfo.Profile.LastName),
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
            users = users.OrderBy(x => x.FirstName).ThenBy(x => x.LastName);
            return new GetAllUsersResponse { Users = users };
        }

        public async Task<IEnumerable<LastOrder>> GetLast5Orders(ClaimsIdentity claimsIdentity)
        {
            var userInfo = await _databaseRepository.GetUserInfo(claimsIdentity.GetUserName());
            return userInfo.Last5Orders;
        }
    }
}