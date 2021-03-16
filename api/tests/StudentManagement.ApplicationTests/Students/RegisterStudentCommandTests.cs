using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class RegisterStudentCommandTests
    {
        private StudentRepositorySpy studentRepositorySpy;
        private readonly UnitOfWorkSpy unitOfWork;
        private NotificationServiceSpy notificationService;
        private RegisterStudentCommandHandler handler;

        public RegisterStudentCommandTests()
        {
            unitOfWork = new UnitOfWorkSpy();
            studentRepositorySpy = unitOfWork.StudentRepositorySpy;
            notificationService = new NotificationServiceSpy();
            handler = new RegisterStudentCommandHandler(unitOfWork, notificationService);
        }

        [Fact]
        public async Task It_Returns_A_Failed_Result_If_Student_Already_Exists()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            await studentRepositorySpy.Add(student);

            var command = new RegisterStudentCommand("Jordan Walker", "walker.jlg@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<StudentAlreadyRegisteredError>(result.Error);
        }

        [Fact]
        public async Task It_Adds_A_Student_To_The_Repository()
        {
            var command = new RegisterStudentCommand("Jordan Walker", "walker.jlg@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.Single(studentRepositorySpy.Students);
            Assert.Equal("Jordan Walker", studentRepositorySpy.Students[0].Name.Name);
            Assert.Equal("walker.jlg@gmail.com", studentRepositorySpy.Students[0].Email.Address);
            Assert.True(unitOfWork.WasCommited);
        }

        [Fact]
        public async Task It_Notifies_The_Student_Of_The_Registration()
        {
            var command = new RegisterStudentCommand("Jordan Walker", "walker.jlg@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            var notification = notificationService.Registrations.FirstOrDefault();
            Assert.NotNull(notification);
            Assert.Single(notificationService.Registrations);
            Assert.Equal(result.Value, notification.Id);
            Assert.Equal("Jordan Walker", notification.Name);
            Assert.Equal("walker.jlg@gmail.com", notification.Email);
        }

        [Fact]
        public async Task It_Returns_A_Student_Id()
        {
            var command = new RegisterStudentCommand("Jordan Walker", "walker.jlg@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.Equal(studentRepositorySpy.Students[0].Id, result.Value);
        }
    }
}
