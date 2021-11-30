using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IDueDatesRepository
    {
        Task<IEnumerable<ParticipationWork>> GetParticipationWorks(int matNr, int courseId);
        Task<IEnumerable<LatedaysLeft>> GetLeftLateDaysForAllWorkCategories(int courseId, int matNr);
    }
}