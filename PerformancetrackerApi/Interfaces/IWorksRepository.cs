using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IWorksRepository
    {
        Task<IEnumerable<StudentWork>> GetCommitedWorksForStudentInCourse(int matNr, int courseId);
        Task<IEnumerable<StudentWork>> GetUncommitedWorksForStudentInCourse(int matNr, int courseId);
    }
}