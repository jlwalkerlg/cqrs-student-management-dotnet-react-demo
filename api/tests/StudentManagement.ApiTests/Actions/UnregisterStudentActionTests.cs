using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Controllers;
using StudentManagement.ApiTests.Doubles;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApiTests.Actions
{
    public class UnregisterStudentActionTests
    {
        private readonly MediatorSpy mediatorSpy;
        private readonly StudentsController controller;

        public UnregisterStudentActionTests()
        {
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok<object>();
            controller = new StudentsController(mediatorSpy);
        }

        [Fact]
        public async Task It_Constructs_The_Command_Properly()
        {
            var id = Guid.NewGuid();
            await controller.Unregister(id);
            var command = mediatorSpy.Request as UnregisterStudentCommand;

            Assert.Equal(id, command.StudentId);
        }

        [Fact]
        public async Task It_Returns_A_204_Response_When_Successful()
        {
            var response = await controller.Unregister(Guid.NewGuid()) as StatusCodeResult;

            Assert.Equal(204, response.StatusCode);
        }
    }
}
