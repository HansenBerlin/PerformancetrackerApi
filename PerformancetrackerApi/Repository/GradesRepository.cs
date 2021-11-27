using System;
using System.Collections.Generic;
using System.Globalization;
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
                $"SELECT l.wert Value, lt.fk_leistungstyp WorkType, aik.frist DueDate, l.abgabe_ist CommitDate, l.id Id " +
                $"FROM leistung l " +
                $"JOIN abgabe_in_kurs aik on l.fk_abgabe_in_kurs = aik.id " +
                $"JOIN leistung_template lt on aik.fk_leistung_template = lt.id " +
                $"WHERE fk_matnr = {matNr} " +
                $"AND fk_kurs = {courseId}";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<StudentGrade>(query);
            return response.ToList();
        }
        
        public async Task<bool> UpdateGrade(int gradeId, double value)
        {
            string query =
                $"UPDATE leistung " +
                $"SET wert = {value.ToString("0.0", CultureInfo.InvariantCulture)} " +
                $"WHERE id = {gradeId}; " +
                $"COMMIT;";
            await using var connection = context.Connection;
            //var response = await connection.QueryAsync<>(query);
            try
            {
                await connection.ExecuteAsync(query);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}