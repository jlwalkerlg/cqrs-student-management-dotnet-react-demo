using System.Linq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using StudentManagement.Domain.Students;
using StudentManagement.Application.Students;
using StudentManagement.Domain.Courses;

namespace StudentManagement.ApiTests.Functional
{
    public class GetStudentApiTest : ApiTestBase
    {
        public GetStudentApiTest(WebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task It_Returns_Validation_Errors_When_Request_Is_Invalid()
        {
            var response = await Get($"/students?numberOfCourses=-1&enrolledIn={Guid.Empty}");
            var json = await response.ToJson();

            Assert.True(response.IsFailure);
            Assert.Equal(422, response.StatusCode);
            Assert.NotNull(json["error"]);
            Assert.NotNull(json["errors"]);
        }

        [Fact]
        public async Task It_Returns_A_List_Of_Student_Dtos()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var philosophy = new Course(
                new CourseTitle("Philosophy"),
                new Credits(1)
            );
            var physics = new Course(
                new CourseTitle("Physics"),
                new Credits(2)
            );
            student.Enroll(philosophy, Grade.A);
            student.Enroll(physics, Grade.B);
            context.Courses.AddRange(philosophy, physics);
            context.Students.Add(student);
            context.SaveChanges();

            var response = await Get("/students");
            var json = await response.ToJson();
            var students = json["data"].ToObject<List<StudentDto>>();

            Assert.True(response.IsSuccess);

            Assert.Single(students);
            Assert.Equal(student.Id, students[0].Id);
            Assert.Equal(student.Name.Name, students[0].Name);
            Assert.Equal(student.Email.Address, students[0].Email);

            var philosophyEnrollment = students[0].Enrollments
                .FirstOrDefault(e => e.Course.Id == philosophy.Id);
            Assert.NotNull(philosophyEnrollment);
            Assert.Equal(philosophy.Id, philosophyEnrollment.Course.Id);
            Assert.Equal(philosophy.Title.Title, philosophyEnrollment.Course.Title);
            Assert.Equal(philosophy.Credits.Amount, philosophyEnrollment.Course.Credits);
            Assert.Equal(Grade.A.ToString(), philosophyEnrollment.Grade);

            var physicsEnrollment = students[0].Enrollments
                .FirstOrDefault(e => e.Course.Id == physics.Id);
            Assert.NotNull(physicsEnrollment);
            Assert.Equal(physics.Id, physicsEnrollment.Course.Id);
            Assert.Equal(physics.Title.Title, physicsEnrollment.Course.Title);
            Assert.Equal(physics.Credits.Amount, physicsEnrollment.Course.Credits);
            Assert.Equal(Grade.B.ToString(), physicsEnrollment.Grade);
        }
    }
}
