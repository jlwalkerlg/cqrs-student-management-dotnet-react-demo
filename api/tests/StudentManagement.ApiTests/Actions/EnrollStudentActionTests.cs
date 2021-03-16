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
    public class EnrollStudentActionTests
    {
        private readonly MediatorSpy mediatorSpy;
        private readonly CoursesController controller;
        private readonly EnrollStudentRequest request;

        public EnrollStudentActionTests()
        {
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok<object>();
            controller = new CoursesController(mediatorSpy);
            request = new EnrollStudentRequest();
        }

        [Fact]
        public async Task It_Constructs_The_Command_Properly()
        {
            var studentId = Guid.NewGuid();
            request.CourseId = Guid.NewGuid();
            request.Grade = "A";

            await controller.Enroll(studentId, request);

            var command = mediatorSpy.Request as EnrollStudentCommand;

            Assert.Equal(studentId, command.StudentId);
            Assert.Equal(request.CourseId, command.CourseId);
            Assert.Equal(request.Grade, command.Grade);
        }

        [Fact]
        public async Task It_Returns_A_201_Response_When_Successful()
        {
            var response = await controller.Enroll(Guid.NewGuid(), request) as StatusCodeResult;

            Assert.Equal(201, response.StatusCode);
        }

        [Fact]
        public async Task It_Returns_Validation_Errors_When_Request_Is_Invalid()
        {
            var validationError = new ValidationError();
            validationError.AddError("StudentId", "Required.");
            validationError.AddError("CourseId", "Required.");
            validationError.AddError("Grade", "Required.");

            mediatorSpy.Response = Result.Fail<object>(validationError);

            var response = await controller.Enroll(Guid.NewGuid(), request) as UnprocessableEntityObjectResult;
            var content = response.Value as InvalidEnvelope;

            Assert.Equal(422, response.StatusCode);
            Assert.True(content.Errors.ContainsKey("studentId"));
            Assert.True(content.Errors.ContainsKey("courseId"));
            Assert.True(content.Errors.ContainsKey("grade"));
        }
    }
}
