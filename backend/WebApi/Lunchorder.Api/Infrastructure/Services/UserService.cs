using System;
using System.Linq;
using System.Threading.Tasks;
using Lunchorder.Common.Interfaces;
using Lunchorder.Domain.Entities.Authentication;
using Microsoft.AspNet.Identity;

namespace Lunchorder.Api.Infrastructure.Services
{
    public class ApplicationUserService : IUserService
    {
        private readonly ApplicationUserManager _userManager;

        public ApplicationUserService(ApplicationUserManager userManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            _userManager = userManager;
        }

        public async Task<string> Create(string userId, string username, string email)
        {
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = username, Email = email };

            IdentityResult result = await _userManager.CreateAsync(user);

            // todo better error handling
            if (result == null)
            {
                throw new Exception("Could not create user, response was null");
            }
            if (result.Errors != null)
            {
                throw new Exception(String.Join(";", result.Errors));
            }

            return user.Id;
        }
    }
}