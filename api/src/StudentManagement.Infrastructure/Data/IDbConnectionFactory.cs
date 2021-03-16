using System.Data;

namespace StudentManagement.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection Connection { get; }
    }
}
