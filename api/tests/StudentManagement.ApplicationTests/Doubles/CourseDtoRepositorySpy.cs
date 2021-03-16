using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class CourseDtoRepositorySpy : ICourseDtoRepository
    {
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();

        public async Task<List<CourseDto>> Get()
        {
            await Task.CompletedTask;
            return Courses;
        }
    }
}
