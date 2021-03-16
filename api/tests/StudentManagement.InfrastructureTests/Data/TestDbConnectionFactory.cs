using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.InfrastructureTests.Data
{
    public class TestDbConnectionFactory : IDbConnectionFactory
    {
        private static readonly string connectionString;

        static TestDbConnectionFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            connectionString = config["DbConnectionString"];
        }

        public IDbConnection Connection { get; private set; }

        public IDbConnection OpenConnection()
        {
            Connection = new MySqlConnection(connectionString);
            Connection.Open();
            return Connection;
        }
    }
}
