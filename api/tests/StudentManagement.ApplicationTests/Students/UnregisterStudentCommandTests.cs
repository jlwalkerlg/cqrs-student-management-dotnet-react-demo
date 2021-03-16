using System;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class UnregisterStudentCommandTests
    {
        private readonly UnitOfWorkSpy unitOfWorkSpy;
        private readonly StudentRepositorySpy studentRepositorySpy;
        private readonly UnregisterStudentCommandHandler handler;

        public UnregisterStudentCommandTests()
        {
            unitOfWorkSpy = new UnitOfWorkSpy();
            studentRepositorySpy = unitOfWorkSpy.StudentRepositorySpy;
            handler = new UnregisterStudentCommandHandler(unitOfWorkSpy);
        }

        [Fact]
        public async Task It_Returns_An_Error_If_Student_Not_Found()
        {
            var command = new UnregisterStudentCommand(Guid.NewGuid());
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
        }

        [Fact]
        public async Task It_Unregisters_The_Student()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            await studentRepositorySpy.Add(student);

            var command = new UnregisterStudentCommand(student.Id);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.Empty(studentRepositorySpy.Students);
            Assert.True(unitOfWorkSpy.WasCommited);
        }
    }
}
