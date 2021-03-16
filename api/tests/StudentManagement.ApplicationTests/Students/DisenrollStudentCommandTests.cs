using System;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Courses;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class DisenrollStudentCommandTests
    {
        private readonly StudentRepositorySpy studentRepositorySpy;
        private readonly CourseRepositorySpy courseRepositorySpy;
        private readonly UnitOfWorkSpy unitOfWorkSpy;
        private readonly DisenrollStudentCommandHandler handler;

        public DisenrollStudentCommandTests()
        {
            unitOfWorkSpy = new UnitOfWorkSpy();
            studentRepositorySpy = unitOfWorkSpy.StudentRepositorySpy;
            courseRepositorySpy = unitOfWorkSpy.CourseRepositorySpy;
            handler = new DisenrollStudentCommandHandler(unitOfWorkSpy);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_Student_Not_Found()
        {
            var course = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            await courseRepositorySpy.Add(course);

            var command = new DisenrollStudentCommand(Guid.NewGuid(), course.Id, "Comment.");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<StudentNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_Course_Not_Found()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            await studentRepositorySpy.Add(student);

            var command = new DisenrollStudentCommand(student.Id, Guid.NewGuid(), "Comment.");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<CourseNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Returns_An_Error_If_Student_Is_Not_Enrolled_In_Course()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var course = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            await studentRepositorySpy.Add(student);
            await courseRepositorySpy.Add(course);

            var command = new DisenrollStudentCommand(student.Id, course.Id, "Comment.");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task It_Enrolls_The_Student()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var course = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            student.Enroll(course, Grade.A);
            await studentRepositorySpy.Add(student);
            await courseRepositorySpy.Add(course);

            var command = new DisenrollStudentCommand(student.Id, course.Id, "Comment.");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.False(student.AlreadyEnrolledIn(course));
            Assert.True(result.IsSuccess);
            Assert.True(unitOfWorkSpy.WasCommited);
        }
    }
}
