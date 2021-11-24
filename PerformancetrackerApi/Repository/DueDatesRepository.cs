using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class DueDatesRepository : IDueDatesRepository
    {
        private readonly DbContext context;

        public DueDatesRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<StudentWork>> GetDueDatesForStudentInCourse(int matNr, int courseId)
        {
            string query =
                $"SELECT frist DueDate, fk_leistungstyp Worktype " +
                $"FROM abgabe_in_kurs a " +
                $"INNER JOIN leistung_template lt ON a.fk_leistung_template = lt.id " +
                $"WHERE a.id " +
                $"NOT IN (" +
                $"SELECT fk_abgabe_in_kurs " +
                $"FROM leistung " +
                $"WHERE fk_matnr = {matNr}) " +
                $"AND fk_kurs = {courseId};";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<StudentWork>(query);
            return response.ToList();
        }
        
        public async Task<IEnumerable<ParticipationWork>> GetParticipationWorks(int matNr, int courseId)
        {
            string query = null;
            if (matNr == 0 && courseId == 0)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten;";
            else if (matNr != 0 && courseId == 0)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE matNr = {matNr};";
            else if (matNr == 0 && courseId != 0)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE fk_kurs = {courseId};";
            else
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE matnr = {matNr} AND fk_kurs = {courseId};";
            
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<ParticipationWork>(query);
            return response.ToList();
        }
        
        

    }
}