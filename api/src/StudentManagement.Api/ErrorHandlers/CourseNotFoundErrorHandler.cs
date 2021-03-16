using StudentManagement.Application.Courses;

namespace StudentManagement.Api.ErrorHandlers
{
    public class CourseNotFoundErrorHandler : ErrorHandlerBase<CourseNotFoundError>
    {
        protected override string Message => "Course not found.";
        protected override int StatusCode => 404;
    }
}
