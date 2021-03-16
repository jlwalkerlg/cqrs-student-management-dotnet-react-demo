using System.Threading.Tasks;
using StudentManagement.Domain.Students;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.Students;
using System;

namespace StudentManagement.Infrastructure.Data.EF.Repositories
{
    public class StudentRepository : Repository, IStudentRepository
    {
        private readonly DbSet<Student> students;

        public StudentRepository(AppDbContext context) : base(context)
        {
            this.students = context.Students;
        }

        public async Task Add(Student student)
        {
            await students.AddAsync(student);
        }

        public async Task<Student> FindByEmail(string email)
        {
            return await students
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Email.Address == email);
        }

        public async Task<Student> FindById(Guid id)
        {
            return await students
                .Include(x => x.Enrollments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task Remove(Student student)
        {
            students.Remove(student);
            return Task.CompletedTask;
        }
    }
}
