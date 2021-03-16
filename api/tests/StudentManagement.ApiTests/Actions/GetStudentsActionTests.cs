using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Controllers;
using StudentManagement.ApiTests.Doubles;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;
using static StudentManagement.Api.Envelope;

namespace StudentManagement.ApiTests.Actions
{
    public class GetStudentsActionTests
    {
        private readonly List<StudentDto> students;
        private readonly MediatorSpy mediatorSpy;
        private readonly StudentsController controller;

        public GetStudentsActionTests()
        {
            students = new List<StudentDto>
            {
                new StudentDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Jordan Walker",
                    Email = "walker.jlg@gmail.com",
                },
            };
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok(students);
            controller = new StudentsController(mediatorSpy);
        }

        [Fact]
        public async Task It_Constructs_The_Query_Properly()
        {
            var numberOfCourses = 1;
            var courseId = Guid.NewGuid();
            await controller.Get(numberOfCourses, courseId);

            var query = mediatorSpy.Request as GetStudentsQuery;
            Assert.Equal(numberOfCourses, query.NumberOfCourses.Value);
            Assert.Equal(courseId, query.EnrolledIn.Value);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Students()
        {
            var response = await controller.Get(null, null) as ObjectResult;

            var content = response.Value as SuccessEnvelope;

            Assert.Equal(200, response.StatusCode);
            Assert.Same(students, content.Data);
        }

        [Fact]
        public async Task It_Returns_Validation_Errors_When_Request_Is_Invalid()
        {
            var validationError = new ValidationError();
            validationError.AddError("NumberOfCourses", "Required.");
            validationError.AddError("EnrolledIn", "Required.");

            mediatorSpy.Response = Result.Fail<List<StudentDto>>(validationError);

            var response = await controller.Get(null, null) as UnprocessableEntityObjectResult;
            var content = response.Value as InvalidEnvelope;

            Assert.Equal(422, response.StatusCode);
            Assert.True(content.Errors.ContainsKey("numberOfCourses"));
            Assert.True(content.Errors.ContainsKey("enrolledIn"));
        }
    }
}
