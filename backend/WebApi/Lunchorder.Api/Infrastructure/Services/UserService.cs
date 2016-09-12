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
        private readonly Func<UserManager<ApplicationUser>> _userManager;

        public ApplicationUserService(Func<UserManager<ApplicationUser>> userManager)
        {
            if (userManager == null) throw new ArgumentNullException(nameof(userManager));
            _userManager = userManager;
        }

        /// <summary>
        /// Creates a new user without a password
        /// </summary>
        /// <param name="username">New username</param>
        /// <param name="email">Email of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <returns></returns>
        public async Task<ApplicationUser> Create(string username, string email, string firstName, string lastName)
        {
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), UserName = username, Email = email, FirstName = firstName, LastName = lastName };
            IdentityResult result = await _userManager().CreateAsync(user);

            HandleError(result);

            return user;
        }

        /// <summary>
        /// Creates a new user with a password
        /// </summary>
        /// <param name="username">New username</param>
        /// <param name="email">Email of the user</param>
        /// <param name="firstName">First name of the user</param>
        /// <param name="lastName">Last name of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns></returns>
        public async Task<ApplicationUser> Create(string username, string email, string firstName, string lastName, string password)
        {
            
            
            var user = new ApplicationUser { Id = Guid.NewGuid().ToString(), Balance = new Random().Next(100), UserName = username, Email = email, FirstName = firstName, LastName = lastName };
            IdentityResult result = await _userManager().CreateAsync(user, password);

            HandleError(result);

            return user;
        }

        private void HandleError(IdentityResult result)
        {
            // todo better error handling
            if (result == null)
            {
                throw new Exception("Could not create user, response was null");
            }
            if (result.Errors != null && result.Errors.Any())
            {
                throw new Exception(string.Join(";", result.Errors));
            }
        }
    }
}