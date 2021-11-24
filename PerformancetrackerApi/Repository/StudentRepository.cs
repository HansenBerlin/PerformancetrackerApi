using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DbContext context;

        public StudentRepository(DbContext context)
        {
            this.context = context;
        }
        
        public async Task<IEnumerable<Student>> GetStudentsInCourse(int courseId)
        {
            string query = $"SELECT mail Mail, vorname FirstName, nachname LastName, matnr MatNr " +
                           $"FROM student_in_kurs sk " +
                           $"JOIN student s " +
                           $"ON sk.fk_matnr = s.matnr " +
                           $"JOIN person p " +
                           $"ON p.mail = s.fk_mail " +
                           $"WHERE fk_kurs = {courseId} " +
                           $"ORDER BY nachname;";
            await using var connection = context.Connection;
            var students = await connection.QueryAsync<Student>(query);
            return students.ToList();
        }
    }
}