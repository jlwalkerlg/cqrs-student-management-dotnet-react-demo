using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentManagement.Application.Students
{
    public interface IStudentDtoRepository
    {
        Task<List<StudentDto>> GetStudents(GetStudentsFilter filter);
    }
}
