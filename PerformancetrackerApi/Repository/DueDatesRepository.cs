using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
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

        
        
        public async Task<IEnumerable<ParticipationWork>> GetParticipationWorks(int matNr, int courseId)
        {
            string query = null;
            if (matNr == -1 && courseId == -1)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten;";
            else if (matNr != -1 && courseId == -1)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE matNr = {matNr};";
            else if (matNr == -1 && courseId != -1)
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE fk_kurs = {courseId};";
            else
                query = $"SELECT vorname FirstName, nachname LastName, nachweis Link, bezeichnung Description, zeitpunkt CommitDate " +
                        $"FROM aktive_mitarbeit_von_studenten WHERE matnr = {matNr} AND fk_kurs = {courseId};";
            
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<ParticipationWork>(query);
            return response.ToList();
        }
        
        public async Task<IEnumerable<LatedaysLeft>> GetLeftLateDaysForAllWorkCategories(int courseId, int matNr)
        {
            string query = $"call berechne_latedays({courseId}, {matNr});";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<LatedaysLeft>(query);
            return response.ToList();
        }



    }
}