using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IDueDatesRepository
    {
        Task<IEnumerable<StudentWork>> GetDueDatesForStudentInCourse(int matNr, int courseId);
        Task<IEnumerable<ParticipationWork>> GetParticipationWorks(int matNr, int courseId);
    }
}