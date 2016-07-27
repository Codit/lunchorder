using System;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IMenuControllerService
    {
        Task<Menu> GetActiveMenu();
        Task Add(Menu menu);
        Task Update(Menu menu);
        Task Delete(string menuId);
        Task SetActive(string menuId);
    }
}