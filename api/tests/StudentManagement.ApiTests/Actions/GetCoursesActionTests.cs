using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.Controllers;
using StudentManagement.ApiTests.Doubles;
using StudentManagement.Application;
using StudentManagement.Application.Courses;
using Xunit;
using static StudentManagement.Api.Envelope;

namespace StudentManagement.ApiTests.Actions
{
    public class GetCoursesActionTests
    {
        private readonly List<CourseDto> courses;
        private readonly MediatorSpy mediatorSpy;
        private readonly CoursesController controller;

        public GetCoursesActionTests()
        {
            courses = new List<CourseDto>
            {
                new CourseDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Philosophy",
                    Credits = 1,
                },
            };
            mediatorSpy = new MediatorSpy();
            mediatorSpy.Response = Result.Ok(courses);
            controller = new CoursesController(mediatorSpy);
        }

        [Fact]
        public async Task It_Constructs_The_Query_Properly()
        {
            await controller.Get();

            Assert.IsType<GetCoursesQuery>(mediatorSpy.Request);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Courses()
        {
            var response = await controller.Get() as ObjectResult;

            var content = response.Value as SuccessEnvelope;

            Assert.Equal(200, response.StatusCode);
            Assert.Same(courses, content.Data);
        }
    }
}
