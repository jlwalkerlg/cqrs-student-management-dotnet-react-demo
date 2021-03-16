using System.Collections.Generic;
using System;

namespace StudentManagement.Application.Students
{
    public class StudentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<EnrollmentDto> Enrollments { get; set; } = new List<EnrollmentDto>();
    }
}
