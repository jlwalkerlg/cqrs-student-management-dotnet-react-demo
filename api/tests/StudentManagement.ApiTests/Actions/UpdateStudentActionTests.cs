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
    public class UpdateStudentActionTests
    {
        private readonly MediatorSpy mediatorSpy;
        private readonly StudentsController controller;
        private readonly UpdateStudentRequest request;

        public UpdateStudentActionTests()
        {
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok<object>();
            controller = new StudentsController(mediatorSpy);
            request = new UpdateStudentRequest();
        }

        [Fact]
        public async Task It_Constructs_The_Command_Properly()
        {
            var id = Guid.NewGuid();
            request.Name = "Jordan Walker";
            request.Email = "walker.jlg@gmail.com";

            await controller.Update(id, request);
            var command = mediatorSpy.Request as EditStudentDetailsCommand;

            Assert.Equal(id, command.Id);
            Assert.Equal(request.Name, command.Name);
            Assert.Equal(request.Email, command.Email);
        }

        [Fact]
        public async Task It_Returns_A_200_Response_When_Successful()
        {
            var response = await controller.Update(Guid.NewGuid(), request) as StatusCodeResult;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task It_Returns_Validation_Errors_When_Request_Is_Invalid()
        {
            var validationError = new ValidationError();
            validationError.AddError("Name", "Required.");
            validationError.AddError("Email", "Required.");

            mediatorSpy.Response = Result.Fail<object>(validationError);

            var response = await controller.Update(Guid.NewGuid(), request) as UnprocessableEntityObjectResult;
            var content = response.Value as InvalidEnvelope;

            Assert.Equal(422, response.StatusCode);
            Assert.True(content.Errors.ContainsKey("name"));
            Assert.True(content.Errors.ContainsKey("name"));
        }
    }
}
