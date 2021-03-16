using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.ErrorHandlers;
using StudentManagement.Api.Filters;
using StudentManagement.Api.Presenters;
using StudentManagement.Application;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using Controller = Microsoft.AspNetCore.Mvc.ControllerBase;

namespace StudentManagement.Api.Controllers
{
    [TypeFilter(typeof(UnhandledExceptionFilter))]
    public abstract class ControllerBase : Controller
    {
        private static Dictionary<Type, Type> handlers = new Dictionary<Type, Type>
        {
            { typeof(StudentNotFoundError), typeof(StudentNotFoundErrorHandler) },
            { typeof(CourseNotFoundError), typeof(CourseNotFoundErrorHandler) },
            { typeof(StudentAlreadyRegisteredError), typeof(StudentAlreadyRegisteredErrorHandler) },
        };

        protected IActionResult FromPresenter(Presenter presenter)
        {
            if (presenter.IsSuccess)
            {
                if (presenter.Content == null)
                    return StatusCode(presenter.SuccessStatusCode);

                return StatusCode(presenter.SuccessStatusCode, Envelope.Ok(presenter.Content));
            }

            if (presenter.Errors != null)
                return UnprocessableEntity(Envelope.Invalid(presenter.Errors));

            return HandleError(presenter.Error);
        }

        private IActionResult HandleError(Error error)
        {
            var type = error.GetType();

            if (handlers.ContainsKey(type))
                return GetHandler(type).Handle(error);

            return StatusCode(500, Envelope.Error("Server error."));
        }

        private IErrorHandler GetHandler(Type errorType)
        {
            var handlerType = handlers[errorType];
            var handler = Activator.CreateInstance(handlerType);
            return (IErrorHandler)handler;
        }
    }
}
