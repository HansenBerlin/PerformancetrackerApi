using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class GradesRepository : IGradesRepository
    {
        private readonly DbContext context;

        public GradesRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentGrade>> GetGradesPerStudentInCourse(int matNr, int courseId)
        {
            string query =
                $"SELECT l.wert Value, lt.fk_leistungstyp WorkType, aik.frist DueDate, l.abgabe_ist CommitDate " +
                $"FROM leistung l " +
                $"JOIN abgabe_in_kurs aik on l.fk_abgabe_in_kurs = aik.id " +
                $"JOIN leistung_template lt on aik.fk_leistung_template = lt.id " +
                $"WHERE fk_matnr = {matNr} " +
                $"AND fk_kurs = {courseId}";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<StudentGrade>(query);
            return response.ToList();
        }
    }
}