using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Controllers;
using StudentManagement.Api.Requests;
using StudentManagement.ApiTests.Doubles;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;
using static StudentManagement.Api.Envelope;

namespace StudentManagement.ApiTests.Actions
{
    public class TransferStudentActionTests
    {
        private readonly MediatorSpy mediatorSpy;
        private readonly CoursesController controller;
        private readonly TransferStudentRequest request;

        public TransferStudentActionTests()
        {
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok<object>();
            controller = new CoursesController(mediatorSpy);
            request = new TransferStudentRequest();
        }

        [Fact]
        public async Task It_Constructs_The_Command_Properly()
        {
            var studentId = Guid.NewGuid();
            request.FromCourseId = Guid.NewGuid();
            request.ToCourseId = Guid.NewGuid();
            request.Grade = "A";

            await controller.Transfer(studentId, request);
            var command = mediatorSpy.Request as TransferStudentCommand;

            Assert.Equal(studentId, command.StudentId);
            Assert.Equal(request.FromCourseId, command.FromCourseId);
            Assert.Equal(request.ToCourseId, command.ToCourseId);
            Assert.Equal(request.Grade, command.Grade);
        }

        [Fact]
        public async Task It_Returns_A_200_Response_When_Successful()
        {
            var response = await controller.Transfer(Guid.NewGuid(), request) as StatusCodeResult;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task It_Returns_Validation_Errors_When_Request_Is_Invalid()
        {
            var validationError = new ValidationError();
            validationError.AddError("StudentId", "Required.");
            validationError.AddError("FromCourseId", "Required.");
            validationError.AddError("ToCourseId", "Required.");
            validationError.AddError("Grade", "Required.");

            mediatorSpy.Response = Result.Fail<object>(validationError);

            var response = await controller.Transfer(Guid.NewGuid(), request) as UnprocessableEntityObjectResult;
            var content = response.Value as InvalidEnvelope;

            Assert.Equal(422, response.StatusCode);
            Assert.True(content.Errors.ContainsKey("studentId"));
            Assert.True(content.Errors.ContainsKey("fromCourseId"));
            Assert.True(content.Errors.ContainsKey("toCourseId"));
            Assert.True(content.Errors.ContainsKey("grade"));
        }
    }
}
