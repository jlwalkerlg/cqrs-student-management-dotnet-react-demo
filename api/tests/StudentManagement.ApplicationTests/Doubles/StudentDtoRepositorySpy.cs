using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Application.Students;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class StudentDtoRepositorySpy : IStudentDtoRepository
    {
        public GetStudentsFilter GetStudentsFilter { get; private set; }
        public List<StudentDto> Students { get; } = new List<StudentDto>();

        public async Task<List<StudentDto>> GetStudents(GetStudentsFilter filter)
        {
            GetStudentsFilter = filter;

            await Task.CompletedTask;
            return Students;
        }
    }
}
