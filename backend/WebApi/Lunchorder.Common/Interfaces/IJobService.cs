using System.Threading.Tasks;

namespace Lunchorder.Common.Interfaces
{
    public interface IJobService
    {
        Task RemindUsers();
    }
}