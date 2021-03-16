using System.Threading.Tasks;
using StudentManagement.Application;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using StudentManagement.Infrastructure.Data.EF.Repositories;

namespace StudentManagement.Infrastructure.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            StudentRepository = new StudentRepository(context);
            CourseRepository = new CourseRepository(context);
        }

        public IStudentRepository StudentRepository { get; }
        public ICourseRepository CourseRepository { get; }

        public async Task Commit()
        {
            await context.SaveChangesAsync();
        }
    }
}
