using System.Data;

namespace StudentManagement.Infrastructure.Data.Dapper.Repositories
{
    public abstract class DapperRepository
    {
        public DapperRepository(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        private readonly IDbConnectionFactory connectionFactory;
        protected IDbConnection Connection => connectionFactory.Connection;
    }
}
