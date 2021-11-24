using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetStudentsInCourse(int courseId);
    }
}