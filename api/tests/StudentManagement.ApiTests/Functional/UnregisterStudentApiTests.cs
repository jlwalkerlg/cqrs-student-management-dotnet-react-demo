using Xunit;
using System.Threading.Tasks;
using System;
using StudentManagement.Domain.Students;

namespace StudentManagement.ApiTests.Functional
{
    public class UnregisterStudentApiTests : ApiTestBase
    {
        public UnregisterStudentApiTests(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_An_Error_When_Student_Is_Not_Found()
        {
            var response = await Delete($"/students/{Guid.NewGuid()}");
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(404, response.StatusCode);
            Assert.NotNull(json["error"]);
        }

        [Fact]
        public async Task It_Returns_Success_Status_When_Successful()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            context.Students.Add(student);
            context.SaveChanges();

            var response = await Delete($"/students/{student.Id}");

            Assert.True(response.IsSuccess);
            Assert.Equal(string.Empty, await response.ToString());
        }
    }
}
