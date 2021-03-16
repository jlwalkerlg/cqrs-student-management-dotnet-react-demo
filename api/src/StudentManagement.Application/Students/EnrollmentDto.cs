using StudentManagement.Application.Courses;

namespace StudentManagement.Application.Students
{
    public class EnrollmentDto
    {
        public CourseDto Course { get; set; }
        public string Grade { get; set; }
    }
}
