using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using StudentManagement.Domain.Courses;
using StudentManagement.Domain.Students;

namespace StudentManagement.Infrastructure.Data.EF.Repositories
{
    public class StudentDtoRepository : Repository, IStudentDtoRepository
    {
        private readonly DbSet<Student> students;
        private readonly DbSet<Course> courses;

        public StudentDtoRepository(AppDbContext context) : base(context)
        {
            students = context.Students;
            courses = context.Courses;
        }

        public async Task<List<StudentDto>> GetStudents(GetStudentsFilter filter)
        {
            var query = students.AsQueryable();

            if (filter.NumberOfCourses.HasValue)
            {
                query = query.Where(x => x.Enrollments.Count == filter.NumberOfCourses.Value);
            }

            if (filter.EnrolledIn.HasValue)
            {
                query = query.Where(x =>
                    x.Enrollments.Any(e => e.CourseId == filter.EnrolledIn.Value));
            }

            return await query
                .Include(x => x.Enrollments)
                .Select(x => new StudentDto
                {
                    Id = x.Id,
                    Name = x.Name.Name,
                    Email = x.Email.Address,
                    Enrollments = x.Enrollments.Select(e => new EnrollmentDto
                    {
                        Course = courses
                            .Select(c => new CourseDto
                            {
                                Id = c.Id,
                                Title = c.Title.Title,
                                Credits = c.Credits.Amount,
                            })
                            .First(c => c.Id == e.CourseId),
                        Grade = e.Grade.ToString(),
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}
