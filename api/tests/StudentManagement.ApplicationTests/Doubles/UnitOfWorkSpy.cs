using System.Threading.Tasks;
using StudentManagement.Application;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class UnitOfWorkSpy : IUnitOfWork
    {
        public UnitOfWorkSpy()
        {
            StudentRepositorySpy = new StudentRepositorySpy();
            CourseRepositorySpy = new CourseRepositorySpy();
        }

        public StudentRepositorySpy StudentRepositorySpy { get; }
        public CourseRepositorySpy CourseRepositorySpy { get; }
        public bool WasCommited { get; private set; }

        public IStudentRepository StudentRepository => StudentRepositorySpy;
        public ICourseRepository CourseRepository => CourseRepositorySpy;

        public Task Commit()
        {
            WasCommited = true;
            return Task.CompletedTask;
        }
    }
}
