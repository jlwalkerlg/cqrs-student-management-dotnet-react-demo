using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.ApplicationTests.Doubles;
using Xunit;

namespace StudentManagement.ApplicationTests.Courses
{
    public class GetCoursesQueryTests
    {
        private readonly CourseDtoRepositorySpy repositorySpy;
        private readonly GetCoursesQueryHandler handler;

        public GetCoursesQueryTests()
        {
            repositorySpy = new CourseDtoRepositorySpy();
            handler = new GetCoursesQueryHandler(repositorySpy);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Student_Dtos()
        {
            repositorySpy.Courses.Add(new CourseDto
            {
                Id = Guid.NewGuid(),
                Title = "Philosophy",
                Credits = 1,
            });

            var query = new GetCoursesQuery();
            var result = await handler.Handle(query, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.IsType<List<CourseDto>>(result.Value);
            Assert.Same(repositorySpy.Courses, result.Value);
        }
    }
}
