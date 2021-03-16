using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagement.Application.Students;
using StudentManagement.Domain.Students;

namespace StudentManagement.ApplicationTests.Doubles
{
    public class StudentRepositorySpy : IStudentRepository
    {
        public List<Student> Students { get; } = new List<Student>();

        public Task Add(Student student)
        {
            Students.Add(student);
            return Task.CompletedTask;
        }

        public async Task<Student> FindByEmail(string email)
        {
            await Task.CompletedTask;
            return Students.FirstOrDefault(x => x.Email.Address == email);
        }

        public async Task<Student> FindById(Guid id)
        {
            await Task.CompletedTask;
            return Students.FirstOrDefault(x => x.Id == id);
        }

        public Task Remove(Student student)
        {
            Students.Remove(student);
            return Task.CompletedTask;
        }
    }
}
