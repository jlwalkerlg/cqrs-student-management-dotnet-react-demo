using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Courses;
using StudentManagement.Domain.Courses;

namespace StudentManagement.Infrastructure.Data.EF.Repositories
{
    public class CourseRepository : Repository, ICourseRepository
    {
        private readonly DbSet<Course> courses;

        public CourseRepository(AppDbContext context) : base(context)
        {
            this.courses = context.Courses;
        }

        public async Task<Course> FindById(Guid id)
        {
            return await courses.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
