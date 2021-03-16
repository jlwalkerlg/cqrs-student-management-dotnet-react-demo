using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using StudentManagement.Domain.Courses;
using StudentManagement.Application.Courses;

namespace StudentManagement.ApiTests.Functional
{
    public class GetCoursesApiTest : ApiTestBase
    {
        public GetCoursesApiTest(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Course_Dtos()
        {
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            context.Courses.AddRange(philosophy, physics);
            context.SaveChanges();

            var response = await Get("/courses");
            var json = await response.ToJson();
            var courses = json["data"].ToObject<List<CourseDto>>();

            Assert.True(response.IsSuccess);
            Assert.Equal(2, courses.Count);
            Assert.Equal(philosophy.Id, courses[0].Id);
            Assert.Equal(philosophy.Title.Title, courses[0].Title);
            Assert.Equal(philosophy.Credits.Amount, courses[0].Credits);
            Assert.Equal(physics.Id, courses[1].Id);
            Assert.Equal(physics.Title.Title, courses[1].Title);
            Assert.Equal(physics.Credits.Amount, courses[1].Credits);
        }
    }
}
