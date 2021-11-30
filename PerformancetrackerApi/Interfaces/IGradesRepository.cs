using System.Collections.Generic;
using System.Threading.Tasks;
using PerformancetrackerApi.Entities;

namespace PerformancetrackerApi.Interfaces
{
    public interface IGradesRepository
    {
        Task<IEnumerable<StudentGrade>> GetGradesPerStudentInCourse(int matNr, int courseId);
        Task<bool> UpdateGrade(int gradeId, double value);
        Task<bool> InsertGrade(StudentWork work, int matNr);

    }
}