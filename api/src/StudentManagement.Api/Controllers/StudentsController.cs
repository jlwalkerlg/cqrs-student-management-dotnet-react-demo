using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Requests;
using StudentManagement.Application.Students;
using MediatR;
using StudentManagement.Api.Presenters;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IMediator mediator;

        public StudentsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("/students")]
        public async Task<IActionResult> Register([FromBody] RegisterStudentRequest request)
        {
            var command = new RegisterStudentCommand(request.Name, request.Email);
            var result = await mediator.Send(command);

            return FromPresenter(new RegisterStudentPresenter(result));
        }

        [HttpDelete("/students/{id}")]
        public async Task<IActionResult> Unregister([FromRoute] Guid id)
        {
            var command = new UnregisterStudentCommand(id);
            var result = await mediator.Send(command);

            return FromPresenter(new UnregisterStudentPresenter(result));
        }

        [HttpGet("/students")]
        public async Task<IActionResult> Get([FromQuery] int? numberOfCourses, [FromQuery] Guid? enrolledIn)
        {
            var query = new GetStudentsQuery(numberOfCourses, enrolledIn);
            var result = await mediator.Send(query);

            return FromPresenter(new GetStudentsPresenter(result));
        }

        [HttpPut("/students/{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStudentRequest request)
        {
            var command = new EditStudentDetailsCommand(id, request.Name, request.Email);
            var result = await mediator.Send(command);

            return FromPresenter(new UpdateStudentPresenter(result));
        }
    }
}
