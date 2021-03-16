using System.Linq;
using System;
using Dapper;
using StudentManagement.Application.Students;
using Xunit;
using StudentManagement.Infrastructure.Data.Dapper.Repositories;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;

namespace StudentManagement.InfrastructureTests.Data.Dapper.Repositories
{
    public class DapperStudentDtoRepositoryTests : DapperTestBase
    {
        private DapperStudentDtoRepository repository;

        public DapperStudentDtoRepositoryTests(DatabaseFixture fixture) : base(fixture)
        {
            repository = new DapperStudentDtoRepository(connectionFactory);
        }

        private readonly StudentDto[] seedStudents = new[]
            {
                new StudentDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Jordan Walker",
                    Email = "walker.jlg@gmail.com",
                },
                new StudentDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Walker Jordan",
                    Email = "jlg.walker@gmail.com",
                },
                new StudentDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Bruno",
                    Email = "bruno@gmail.com",
                },
            };

        private readonly CourseDto[] seedCourses = new[]
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
            var seedEnrollments = new[]
            {
                new
                {
                    Id = Guid.NewGuid(),
                    Grade = "A",
                    CourseId = seedCourses[0].Id,
                    StudentId = seedStudents[0].Id,
                },
                new
                {
                    Id = Guid.NewGuid(),
                    Grade = "B",
                    CourseId = seedCourses[1].Id,
                    StudentId = seedStudents[1].Id,
                },
            };

            connection.Execute(
                "INSERT INTO Students (Id, Name, Email) Values (@Id, @Name, @Email);",
                seedStudents);
            connection.Execute(
                "INSERT INTO Courses (Id, Title, Credits) Values (@Id, @Title, @Credits);",
                seedCourses);
            connection.Execute(
                "INSERT INTO Enrollment (Id, Grade, CourseId, StudentId) Values (@Id, @Grade, @CourseId, @StudentId);",
                seedEnrollments);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Student_Dtos()
        {
            SeedDb();

            var filter = new GetStudentsFilter();
            var students = await repository.GetStudents(filter);

            Assert.Equal(seedStudents.Length, students.Count());

            var jordanWalker = students.First(x => x.Id == seedStudents[0].Id);
            var walkerJordan = students.First(x => x.Id == seedStudents[1].Id);

            Assert.Equal("Jordan Walker", jordanWalker.Name);
            Assert.Equal("walker.jlg@gmail.com", jordanWalker.Email);
            Assert.Single(jordanWalker.Enrollments);
            Assert.Equal("A", jordanWalker.Enrollments[0].Grade);
            Assert.Equal("Philosophy", jordanWalker.Enrollments[0].Course.Title);
            Assert.Equal(1, jordanWalker.Enrollments[0].Course.Credits);

            Assert.Equal("Walker Jordan", walkerJordan.Name);
            Assert.Equal("jlg.walker@gmail.com", walkerJordan.Email);
            Assert.Single(walkerJordan.Enrollments);
            Assert.Equal("B", walkerJordan.Enrollments[0].Grade);
            Assert.Equal("Physics", walkerJordan.Enrollments[0].Course.Title);
            Assert.Equal(2, walkerJordan.Enrollments[0].Course.Credits);
        }

        [Fact]
        public async Task Student_Dto_Enrollments_Are_Empty_When_They_Are_Not_Enrolled_In_A_Course()
        {
            SeedDb();

            var filter = new GetStudentsFilter();
            var students = await repository.GetStudents(filter);

            var bruno = students.First(x => x.Id == seedStudents[2].Id);

            Assert.Empty(bruno.Enrollments);
        }

        [Fact]
        public async Task It_Filters_Students_By_Number_Of_Enrollments()
        {
            SeedDb();

            var filter = new GetStudentsFilter
            {
                NumberOfCourses = 0,
            };
            var students = await repository.GetStudents(filter);

            Assert.Single(students);
            Assert.Equal(seedStudents[2].Id, students[0].Id);
        }

        [Fact]
        public async Task It_Filters_Students_By_Course_Enrolled_In()
        {
            SeedDb();

            var filter = new GetStudentsFilter
            {
                EnrolledIn = seedCourses[0].Id,
            };
            var students = await repository.GetStudents(filter);

            Assert.Single(students);
            Assert.Equal(seedStudents[0].Id, students[0].Id);
        }
    }
}
