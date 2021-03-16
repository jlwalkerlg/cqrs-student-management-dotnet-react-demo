using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Students;
using StudentManagement.Api.Requests;
using StudentManagement.Application.Courses;
using MediatR;
using StudentManagement.Api.Presenters;

namespace StudentManagement.Api.Controllers
{
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CoursesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("/courses")]
        public async Task<IActionResult> Get()
        {
            var query = new GetCoursesQuery();
            var result = await mediator.Send(query);

            return FromPresenter(new GetCoursesPresenter(result));
        }

        [HttpPost("/students/{studentId}/courses")]
        public async Task<IActionResult> Enroll(
            [FromRoute] Guid studentId,
            [FromBody] EnrollStudentRequest request)
        {
            var command = new EnrollStudentCommand(studentId, request.CourseId, request.Grade);
            var result = await mediator.Send(command);

            return FromPresenter(new EnrollStudentPresenter(result));
        }

        [HttpPost("/students/{studentId}/disenrollments")]
        public async Task<IActionResult> Disenroll(
            [FromRoute] Guid studentId,
            [FromBody] DisenrollStudentRequest request)
        {
            var command = new DisenrollStudentCommand(studentId, request.CourseId, request.Comment);
            var result = await mediator.Send(command);

            return FromPresenter(new DisenrollStudentPresenter(result));
        }

        [HttpPost("/students/{studentId}/transfers")]
        public async Task<IActionResult> Transfer(
            [FromRoute] Guid studentId,
            [FromBody] TransferStudentRequest request)
        {
            var command = new TransferStudentCommand(studentId, request.FromCourseId, request.ToCourseId, request.Grade);
            var result = await mediator.Send(command);

            return FromPresenter(new TransferStudentPresenter(result));
        }
    }
}
