using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface ICacheService
    {
        Task<Menu> GetMenu();
    }
}