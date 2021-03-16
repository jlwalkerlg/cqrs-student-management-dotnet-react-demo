using System;

namespace StudentManagement.Application.Students
{
    public class GetStudentsFilter
    {
        public int? NumberOfCourses { get; set; }
        public Guid? EnrolledIn { get; set; }
    }
}
