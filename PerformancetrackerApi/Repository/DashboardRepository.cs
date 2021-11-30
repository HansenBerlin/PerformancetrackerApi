using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PerformancetrackerApi.Context;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbContext context;

        public DashboardRepository(DbContext context)
        {
            this.context = context;
        }

        public async Task<Dashboard> GetDashboardForStudent(int matNr)
        {
            string query =
                $"SELECT matnr, grade AggregatedGrade, ld_used LatedaysUsed, rep Penaltys, leistungen_commited WorksCommited, " +
                $"leistungen_open WorksOpen, next_due NextDueDate " +
                $"FROM dashboard_overview WHERE matnr = {matNr};";
            await using var connection = context.Connection;
            var response = await connection.QueryAsync<Dashboard>(query);
            return response.FirstOrDefault();
        }
    }
}