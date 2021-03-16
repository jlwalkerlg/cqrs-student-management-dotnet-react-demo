using System;
using System.Threading.Tasks;
using StudentManagement.Domain.Courses;

namespace StudentManagement.Application.Courses
{
    public interface ICourseRepository
    {
        Task<Course> FindById(Guid id);
    }
}
