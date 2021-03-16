using System;

namespace StudentManagement.Api.Requests
{
    public class EnrollStudentRequest
    {
        public Guid CourseId { get; set; }
        public string Grade { get; set; }
    }
}
