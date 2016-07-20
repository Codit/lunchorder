using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IUserService
    {
        Task<string> Create(string username, string email);
    }
}