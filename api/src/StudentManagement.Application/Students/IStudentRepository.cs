using System;
using System.Threading.Tasks;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public interface IStudentRepository
    {
        Task Add(Student student);
        Task<Student> FindByEmail(string email);
        Task<Student> FindById(Guid id);
        Task Remove(Student student);
    }
}
