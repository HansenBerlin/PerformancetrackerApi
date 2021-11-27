using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Controllers;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Repository;

namespace PerformancetrackerApi.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseOverview>> GetAvailableCoursesForStudent(int matNr);
    }
}