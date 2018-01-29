using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IMenuService
    {
        Task<Menu> GetActiveMenu();
    }
}