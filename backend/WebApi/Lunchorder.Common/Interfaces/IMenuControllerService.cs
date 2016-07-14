using System;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IMenuControllerService
    {
        Task<Menu> Get();
        Task Add(Menu menu);
        Task Update(Menu menu);
        Task Delete(Guid menuId);
    }
}