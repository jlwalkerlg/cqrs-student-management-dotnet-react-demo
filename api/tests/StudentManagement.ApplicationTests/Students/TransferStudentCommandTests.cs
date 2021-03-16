using System.Linq;
using System;
using System.Threading.Tasks;
using StudentManagement.Application.Courses;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Courses;
using StudentManagement.Domain.Students;
using Xunit;
using System.Threading;

namespace StudentManagement.ApplicationTests.Students
{
    public class TransferStudentCommandTests
    {
        private readonly StudentRepositorySpy studentRepositorySpy;
        private readonly CourseRepositorySpy courseRepositorySpy;
        private readonly UnitOfWorkSpy unitOfWorkSpy;
        private readonly TransferStudentCommandHandler handler;

        public TransferStudentCommandTests()
        {
            unitOfWorkSpy = new UnitOfWorkSpy();
            studentRepositorySpy = unitOfWorkSpy.StudentRepositorySpy;
            courseRepositorySpy = unitOfWorkSpy.CourseRepositorySpy;
            handler = new TransferStudentCommandHandler(unitOfWorkSpy);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_Student_Not_Found()
        {
            var course = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            await courseRepositorySpy.Add(course);

            var command = new TransferStudentCommand(Guid.NewGuid(), course.Id, course.Id, "A");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<StudentNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_From_Course_Not_Found()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var toCourse = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            await studentRepositorySpy.Add(student);
            await courseRepositorySpy.Add(toCourse);

            var command = new TransferStudentCommand(student.Id, Guid.NewGuid(), toCourse.Id, "A");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<CourseNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_To_Course_Not_Found()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var fromCourse = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            await studentRepositorySpy.Add(student);
            await courseRepositorySpy.Add(fromCourse);

            var command = new TransferStudentCommand(student.Id, fromCourse.Id, Guid.NewGuid(), "A");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<CourseNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Returns_An_Error_If_Student_Not_Enrolled_In_From_Course()
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

            var command = new TransferStudentCommand(student.Id, course.Id, course.Id, "A");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task It_Transfers_The_Student_To_A_New_Course()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            var fromCourse = new Course(
                new CourseTitle("Philsophy"),
                new Credits(1)
            );
            var toCourse = new Course(
                new CourseTitle("Physics"),
                new Credits(1)
            );
            student.Enroll(fromCourse, Grade.A);
            await studentRepositorySpy.Add(student);
            await courseRepositorySpy.Add(fromCourse);
            await courseRepositorySpy.Add(toCourse);

            var command = new TransferStudentCommand(student.Id, fromCourse.Id, toCourse.Id, "A");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(!student.AlreadyEnrolledIn(fromCourse));
            Assert.True(student.AlreadyEnrolledIn(toCourse));
            Assert.True(result.IsSuccess);
            Assert.True(unitOfWorkSpy.WasCommited);
        }

        [Fact]
        public async Task It_Transfers_The_Student_To_The_Same_Course()
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

            var command = new TransferStudentCommand(student.Id, course.Id, course.Id, "B");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(student.AlreadyEnrolledIn(course));
            Assert.Equal(Grade.B, student.Enrollments.First(x => x.CourseId == course.Id).Grade);
            Assert.True(result.IsSuccess);
            Assert.True(unitOfWorkSpy.WasCommited);
        }
    }
}
