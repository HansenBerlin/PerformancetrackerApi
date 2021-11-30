using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class WorksRepository : IWorksRepository
    {
        private readonly DbContext context;

        public WorksRepository(DbContext context)
        {
            this.context = context;
        }
        
        public async Task<IEnumerable<StudentWork>> GetCommitedWorksForStudentInCourse(int matNr, int courseId)
        {
            string query =
                $"SELECT verbrauchteld UsedLatedays, fk_leistungstyp Type, frist DueDate, l.abgabe_ist CommitDate, l.wert Grade, l.id Id, l.festgesetzt Fixed " +
                $"FROM latedays_merged_overvies lo " +
                $"JOIN abgabe_in_kurs ak ON ak.id = lo.abgabe_id " +
                $"JOIN leistung l ON lo.leistung_id = l.id " +
                $"WHERE kurs_id = {courseId} " +
                $"AND lo.fk_matnr = {matNr};";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<StudentWork>(query);
            return response.ToList();
        }
        
        public async Task<IEnumerable<StudentWork>> GetUncommitedWorksForStudentInCourse(int matNr, int courseId)
        {
            string query = $"CALL show_due_dates_with_late_days({matNr}, {courseId})";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<StudentWork>(query);
            return response.ToList();
        }
    }
}