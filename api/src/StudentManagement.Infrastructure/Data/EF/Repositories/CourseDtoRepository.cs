using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Courses;
using StudentManagement.Domain.Courses;

namespace StudentManagement.Infrastructure.Data.EF.Repositories
{
    public class CourseDtoRepository : Repository, ICourseDtoRepository
    {
        private readonly DbSet<Course> courses;

        public CourseDtoRepository(AppDbContext context) : base(context)
        {
            courses = context.Courses;
        }

        public async Task<List<CourseDto>> Get()
        {
            return await courses
                .Select(x => new CourseDto
                {
                    Id = x.Id,
                    Title = x.Title.Title,
                    Credits = x.Credits.Amount,
                })
                .ToListAsync();
        }
    }
}
