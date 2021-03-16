using StudentManagement.Application.Students;

namespace StudentManagement.Api.ErrorHandlers
{
    public class StudentNotFoundErrorHandler : ErrorHandlerBase<StudentNotFoundError>
    {
        protected override string Message => "Student not found.";
        protected override int StatusCode => 404;
    }
}
