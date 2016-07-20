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
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(UserManager<ApplicationUser> userManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            _userManager = userManager;
        }

        public async Task<string> Create(string username, string email)
        {
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = username, Email = email };
            //await _userManager.CreateIdentityAsync(user, "waad");
            IdentityResult result = await _userManager.CreateAsync(user);

            // todo better error handling
            if (result == null)
            {
                throw new Exception("Could not create user, response was null");
            }
            if (result.Errors != null && result.Errors.Any())
            {
                throw new Exception(string.Join(";", result.Errors));
            }

            return user.Id;
        }
    }
}