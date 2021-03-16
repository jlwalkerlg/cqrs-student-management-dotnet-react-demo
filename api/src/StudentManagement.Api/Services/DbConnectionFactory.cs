using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace StudentManagement.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private static readonly string connectionString;

        static DbConnectionFactory()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .Build();

            connectionString = config["DbConnectionString"];
        }

        public IDbConnection Connection => new MySqlConnection(connectionString);
    }
}
