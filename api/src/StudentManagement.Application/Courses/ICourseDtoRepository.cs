using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Application.Courses
{
    public interface ICourseDtoRepository
    {
        Task<List<CourseDto>> Get();
    }
}
