using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StudentManagement.Api.Filters
{
    public class UnhandledExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var result = new ObjectResult(Envelope.Error("Server error."));
            result.StatusCode = 500;

            context.Result = result;
        }
    }
}
