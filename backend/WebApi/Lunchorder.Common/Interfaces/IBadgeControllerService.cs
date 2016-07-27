using System.Collections.Generic;
using System.Threading.Tasks;
using Lunchorder.Domain.Dtos;

namespace Lunchorder.Common.Interfaces
{
    public interface IBadgeControllerService
    {
        Task<IEnumerable<Badge>> Get();
    }
}