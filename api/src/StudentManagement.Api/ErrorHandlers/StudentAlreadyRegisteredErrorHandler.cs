using StudentManagement.Application.Students;

namespace StudentManagement.Api.ErrorHandlers
{
    public class StudentAlreadyRegisteredErrorHandler : ErrorHandlerBase<StudentAlreadyRegisteredError>
    {
        protected override string Message => "Student already registered.";
        protected override int StatusCode => 400;
    }
}
