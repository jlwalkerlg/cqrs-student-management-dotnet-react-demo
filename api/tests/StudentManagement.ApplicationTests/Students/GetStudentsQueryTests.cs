using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class GetStudentsQueryTests
    {
        private readonly StudentDtoRepositorySpy repositorySpy;
        private readonly GetStudentsQueryHandler handler;

        public GetStudentsQueryTests()
        {
            repositorySpy = new StudentDtoRepositorySpy();
            handler = new GetStudentsQueryHandler(repositorySpy);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Student_Dtos()
        {
            repositorySpy.Students.Add(new StudentDto
            {
                Id = Guid.NewGuid(),
                Name = "Jordan Walker",
                Email = "walker.jlg@gmail.com",
                Enrollments = new List<EnrollmentDto>
                {
                    new EnrollmentDto
                    {
                        Course = new CourseDto
                        {
                            Id = Guid.NewGuid(),
                            Title = "Philosophy",
                            Credits = 1,
                        },
                        Grade = Grade.A.ToString()
                    }
                }
            });

            var query = new GetStudentsQuery();
            var result = await handler.Handle(query, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.IsType<List<StudentDto>>(result.Value);
            Assert.Same(repositorySpy.Students, result.Value);
        }

        [Fact]
        public async Task It_Filters_Students()
        {
            var courseId = Guid.NewGuid();
            var query = new GetStudentsQuery(1, courseId);
            var result = await handler.Handle(query, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.Equal(1, repositorySpy.GetStudentsFilter.NumberOfCourses.Value);
            Assert.Equal(courseId, repositorySpy.GetStudentsFilter.EnrolledIn.Value);
        }
    }
}
