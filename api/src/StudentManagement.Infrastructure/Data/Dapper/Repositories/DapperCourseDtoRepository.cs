using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using StudentManagement.Application.Courses;

namespace StudentManagement.Infrastructure.Data.Dapper.Repositories
{
    public class DapperCourseDtoRepository : DapperRepository, ICourseDtoRepository
    {
        public DapperCourseDtoRepository(IDbConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public async Task<List<CourseDto>> Get()
        {
            using (IDbConnection conn = Connection)
            {
                var query = @"
                    SELECT
                        c.Id,
                        c.Title,
                        c.Credits
                    FROM
                        Courses c";

                var courses = await conn.QueryAsync<CourseDto>(query);
                return courses.ToList();
            }
        }
    }
}
