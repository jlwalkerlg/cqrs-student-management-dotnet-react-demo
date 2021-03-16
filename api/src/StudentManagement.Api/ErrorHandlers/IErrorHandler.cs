using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application;

namespace StudentManagement.Api.ErrorHandlers
{
    public interface IErrorHandler
    {
        IActionResult Handle(Error error);
    }
}
