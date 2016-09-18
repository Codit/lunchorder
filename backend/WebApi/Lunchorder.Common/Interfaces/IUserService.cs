using System.Threading.Tasks;
using Lunchorder.Domain.Entities.Authentication;

namespace Lunchorder.Common.Interfaces
{
    public interface IUserService
    {
        Task<ApplicationUser> Create(string username, string email, string firstName, string lastName);
        Task<ApplicationUser> Create(string username, string email, string firstName, string lastName, string password);
        Task UpdateUserPicture(string getUserId, string url);
    }
}