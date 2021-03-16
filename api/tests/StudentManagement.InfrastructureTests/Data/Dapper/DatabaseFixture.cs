using Xunit;

namespace StudentManagement.InfrastructureTests.Data.Dapper
{
    public class DatabaseFixture
    {
        public TestDbConnectionFactory ConnectionFactory { get; }

        public DatabaseFixture()
        {
            ConnectionFactory = new TestDbConnectionFactory();
        }
    }

    [CollectionDefinition("Dapper")]
    public class DapperCollectionFixture : ICollectionFixture<DatabaseFixture>
    {
    }
}
