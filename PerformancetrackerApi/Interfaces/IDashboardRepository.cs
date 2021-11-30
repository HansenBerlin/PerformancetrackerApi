using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IDashboardRepository
    {
        Task<Dashboard> GetDashboardForStudent(int matNr);
    }
}