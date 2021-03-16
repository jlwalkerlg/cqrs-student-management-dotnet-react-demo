using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Controllers;
using StudentManagement.Api.Presenters;
using StudentManagement.Api.Requests;
using StudentManagement.ApiTests.Doubles;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;
using static StudentManagement.Api.Envelope;

namespace StudentManagement.ApiTests.Actions
{
    public class RegisterStudentActionTests
    {
        private readonly MediatorSpy mediatorSpy;
        private readonly StudentsController controller;
        private readonly RegisterStudentRequest request;

        public RegisterStudentActionTests()
        {
            mediatorSpy = new MediatorSpy();
            controller = new StudentsController(mediatorSpy);
            request = new RegisterStudentRequest();
        }

        [Fact]
        public async Task It_Constructs_The_Command_Properly()
        {
            request.Name = "Jordan Walker";
            request.Email = "walker.jlg@gmail.com";

            mediatorSpy.Response = Result.Ok(Guid.NewGuid());
            await controller.Register(request);
            var command = mediatorSpy.Request as RegisterStudentCommand;

            Assert.Equal(request.Name, command.StudentName);
            Assert.Equal(request.Email, command.StudentEmailAddress);
        }

        [Fact]
        public async Task It_Returns_An_Id_With_The_Response_When_Successful()
        {
            var id = Guid.NewGuid();
            mediatorSpy.Response = Result.Ok(id);

            var response = await controller.Register(request) as ObjectResult;
            var content = response.Value as SuccessEnvelope;
            var data = content.Data as RegisterStudentResponse;

            Assert.Equal(201, response.StatusCode);
            Assert.Equal(id, data.Id);
        }

        [Fact]
        public async Task It_Returns_Validation_Error_When_Request_Is_Invalid()
        {
            var validationError = new ValidationError();
            validationError.AddError("Id", "Required.");

            mediatorSpy.Response = Result.Fail<Guid>(validationError);
            var response = await controller.Register(request) as UnprocessableEntityObjectResult;
            var content = response.Value as InvalidEnvelope;

            Assert.Equal(422, response.StatusCode);
            Assert.True(content.Errors.ContainsKey("Id"));
        }
    }
}
