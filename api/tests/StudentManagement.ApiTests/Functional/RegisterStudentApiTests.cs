using Xunit;
using System.Threading.Tasks;
using System;
using StudentManagement.Domain.Students;

namespace StudentManagement.ApiTests.Functional
{
    public class RegisterStudentApiTests : ApiTestBase
    {
        public RegisterStudentApiTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Request_Is_Invalid()
        {
            var response = await Post("/students", new { });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(422, response.StatusCode);
            Assert.NotNull(json["error"]);
            Assert.NotNull(json["errors"]);
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Student_Is_Already_Registered()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var response = await Post("/students", new
            {
                name = "Jordan Walker",
                email = "walker.jlg@gmail.com"
            });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(400, response.StatusCode);
            Assert.NotNull(json["error"]);
        }

        [Fact]
        public async Task It_Returns_A_Student_Id_When_Successful()
        {
            var response = await Post("/students", new
            {
                name = "Jordan Walker",
                email = "walker.jlg@gmail.com"
            });
            var json = await response.ToJson();
            var studentId = json["data"]["id"]?.ToString();

            Assert.True(response.IsSuccess);
            Assert.True(Guid.TryParse(studentId, out Guid id));
            Assert.NotEqual(Guid.Empty, id);
        }
    }
}
