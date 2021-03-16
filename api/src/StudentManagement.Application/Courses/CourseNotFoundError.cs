namespace StudentManagement.Application.Courses
{
    public class CourseNotFoundError : Error
    {
        public CourseNotFoundError() : base("Course not found.")
        {
        }
    }
}
