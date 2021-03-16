using Xunit;
using System.Threading.Tasks;
using System;
using StudentManagement.Domain.Students;
using StudentManagement.Domain.Courses;

namespace StudentManagement.ApiTests.Functional
{
    public class DisenrollStudentApiTests : ApiTestBase
    {
        public DisenrollStudentApiTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Request_Is_Invalid()
        {
            var response = await Post($"/students/{Guid.NewGuid()}/disenrollments", new { });
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

            var response = await Post($"/students/{Guid.NewGuid()}/disenrollments", new
            {
                CourseId = course.Id,
                Comment = "Comment.",
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

            var response = await Post($"/students/{student.Id}/disenrollments", new
            {
                CourseId = Guid.NewGuid(),
                Comment = "Comment.",
            });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(404, response.StatusCode);
            Assert.NotNull(json["error"]);
        }
    }
}
