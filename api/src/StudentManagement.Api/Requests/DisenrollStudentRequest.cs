using System;

namespace StudentManagement.Api.Requests
{
    public class DisenrollStudentRequest
    {
        public Guid CourseId { get; set; }
        public string Comment { get; set; }
    }
}
