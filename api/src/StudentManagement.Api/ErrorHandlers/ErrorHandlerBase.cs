using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application;

namespace StudentManagement.Api.ErrorHandlers
{
    public abstract class ErrorHandlerBase<T> : IErrorHandler where T : Error
    {
        protected abstract string Message { get; }
        protected abstract int StatusCode { get; }

        public IActionResult Handle(Error error)
        {
            Process((T)error);

            var result = new JsonResult(Envelope.Error(Message));
            result.StatusCode = StatusCode;
            return result;
        }

        protected virtual void Process(T error)
        {
        }
    }
}
