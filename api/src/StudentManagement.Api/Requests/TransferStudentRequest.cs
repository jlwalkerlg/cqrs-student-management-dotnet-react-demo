using System;

namespace StudentManagement.Api.Requests
{
    public class TransferStudentRequest
    {
        public Guid FromCourseId { get; set; }
        public Guid ToCourseId { get; set; }
        public string Grade { get; set; }
    }
}
