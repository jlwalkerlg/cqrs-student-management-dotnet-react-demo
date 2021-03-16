using Xunit;
using System.Threading.Tasks;
using System;
using StudentManagement.Domain.Students;
using StudentManagement.Domain.Courses;

namespace StudentManagement.ApiTests.Functional
{
    public class EnrollStudentApiTests : ApiTestBase
    {
        public EnrollStudentApiTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Request_Is_Invalid()
        {
            var response = await Post($"/students/{Guid.NewGuid()}/courses", new { });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(422, response.StatusCode);
            Assert.NotNull(json["error"]);
            Assert.NotNull(json["errors"]);
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Student_Is_Not_Found()
        {
            var course = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            context.Courses.Add(course);
            context.SaveChanges();

            var response = await Post($"/students/{Guid.NewGuid()}/courses", new
            {
                CourseId = course.Id,
                Grade = "A",
            });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(404, response.StatusCode);
            Assert.NotNull(json["error"]);
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Course_Is_Not_Found()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var response = await Post($"/students/{student.Id}/courses", new
            {
                CourseId = Guid.NewGuid(),
                Grade = "A",
            });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(404, response.StatusCode);
            Assert.NotNull(json["error"]);
        }

        [Fact]
        public async Task It_Returns_A_200_When_Successful()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var course = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            context.Students.Add(student);
            context.Courses.Add(course);
            context.SaveChanges();

            var response = await Post($"/students/{student.Id}/courses", new
            {
                CourseId = course.Id,
                Grade = "A",
            });

            Assert.True(response.IsSuccess);
        }
    }
}
