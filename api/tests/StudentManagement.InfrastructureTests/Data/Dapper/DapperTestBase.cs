using System.Data;
using Xunit;

namespace StudentManagement.InfrastructureTests.Data.Dapper
{
    [Collection("Dapper")]
    public abstract class DapperTestBase
    {
        protected readonly TestDbConnectionFactory connectionFactory;
        protected readonly IDbConnection connection;

        public DapperTestBase(DatabaseFixture fixture)
        {
            connectionFactory = fixture.ConnectionFactory;

            connection = connectionFactory.OpenConnection();
            connection.BeginTransaction();
        }
    }
}
