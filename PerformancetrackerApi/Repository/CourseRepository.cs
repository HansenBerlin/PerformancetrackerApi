using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Controllers;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DbContext context;

        public CourseRepository(DbContext context)
        {
            this.context = context;
        }
        
        public async Task<IEnumerable<CourseOverview>> GetAvailableCoursesForStudent(int matNr)
        {
            string query =
                $"SELECT id Id, mail ProfMail, fullname FullName, kursbeschreibung ShortDescription, " +
                $"kurs_kurzform DescriptionCourse, modulbeschreibung DescriptionModule " +
                $"FROM kurs_overview ko " +
                $"JOIN persons_overview p ON ko.fk_mail = p.mail " +
                $"JOIN student_in_kurs sik ON ko.id = sik.fk_kurs " +
                $"WHERE fk_matnr = {matNr} " +
                $"GROUP BY id;";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<CourseOverview>(query);
            return response.ToList();
        }
    }
}