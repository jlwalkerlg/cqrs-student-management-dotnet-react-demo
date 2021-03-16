using System;
using StudentManagement.Domain.Students;

namespace StudentManagement.Application.Students
{
    public class StudentRegisteredNotification : INotification
    {
        public StudentRegisteredNotification(Student student)
        {
            Id = student.Id;
            Name = student.Name.Name;
            Email = student.Email.Address;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
    }
}
