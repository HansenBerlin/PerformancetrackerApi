using System;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace PerformancetrackerApi.Context
{
    public class DbContext : IDisposable
    {
        public MySqlConnection Connection { get; }

        public DbContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}