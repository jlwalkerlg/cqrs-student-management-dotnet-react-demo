using System.Linq;
using System;
using Dapper;
using Xunit;
using StudentManagement.Infrastructure.Data.Dapper.Repositories;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;

namespace StudentManagement.InfrastructureTests.Data.Dapper.Repositories
{
    public class DapperCourseDtoRepositoryTests : DapperTestBase
    {
        private DapperCourseDtoRepository repository;

        public DapperCourseDtoRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
            repository = new DapperCourseDtoRepository(connectionFactory);
        }

        private CourseDto[] seedCourses = new[]
            {
                new CourseDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Philosophy",
                    Credits = 1,
                },
                new CourseDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Physics",
                    Credits = 2,
                },
            };

        private void SeedDb()
        {
            connection.Execute(
                "INSERT INTO Courses (Id, Title, Credits) Values (@Id, @Title, @Credits);",
                seedCourses);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Student_Dtos()
        {
            SeedDb();

            var courses = await repository.Get();

            Assert.Equal(2, courses.Count());

            var philosophy = courses.First(x => x.Title == "Philosophy");
            var physics = courses.First(x => x.Title == "Physics");

            Assert.Equal(1, philosophy.Credits);
            Assert.Equal(2, physics.Credits);
        }
    }
}
