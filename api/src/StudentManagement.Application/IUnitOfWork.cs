using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;

namespace StudentManagement.Application
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }
        ICourseRepository CourseRepository { get; }

        Task Commit();
    }
}
