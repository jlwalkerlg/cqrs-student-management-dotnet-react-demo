using Xunit;
using System.Threading.Tasks;
using System;
using StudentManagement.Domain.Students;

namespace StudentManagement.ApiTests.Functional
{
    public class UpdateStudentApiTests : ApiTestBase
    {
        public UpdateStudentApiTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Request_Is_Invalid()
        {
            var id = Guid.NewGuid();
            var response = await Put($"/students/{id}", new { });
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(422, response.StatusCode);
            Assert.NotNull(json["error"]);
            Assert.NotNull(json["errors"]);
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Student_Is_Not_Found()
        {
            var id = Guid.NewGuid();
            var response = await Put($"/students/{id}", new
            {
                name = "Jordan Walker",
                email = "walker.jlg@gmail.com"
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
            context.Students.Add(student);
            context.SaveChanges();

            var response = await Put($"/students/{student.Id}", new
            {
                name = "Walker Jordan",
                email = "jlg.walker@gmail.com"
            });
            System.Console.WriteLine(await response.ToString());

            Assert.True(response.IsSuccess);
        }
    }
}
