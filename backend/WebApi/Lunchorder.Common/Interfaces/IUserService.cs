using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> Create(string userId, string username, string email);
    }
}