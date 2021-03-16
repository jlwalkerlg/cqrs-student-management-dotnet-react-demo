using System;
using System.Threading;
using System.Threading.Tasks;
using StudentManagement.Application.Students;
using StudentManagement.ApplicationTests.Doubles;
using StudentManagement.Domain.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class EditStudentDetailsCommandTests
    {
        private readonly StudentRepositorySpy studentRepositorySpy;
        private readonly UnitOfWorkSpy unitOfWorkSpy;
        private readonly EditStudentDetailsCommandHandler handler;

        public EditStudentDetailsCommandTests()
        {
            unitOfWorkSpy = new UnitOfWorkSpy();
            studentRepositorySpy = unitOfWorkSpy.StudentRepositorySpy;
            handler = new EditStudentDetailsCommandHandler(unitOfWorkSpy);
        }

        [Fact]
        public async Task It_Returns_A_Failure_If_Student_Not_Found()
        {
            var command = new EditStudentDetailsCommand(Guid.NewGuid(), "Walker Jordan", "jlg.walker@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsFailure);
            Assert.IsType<StudentNotFoundError>(result.Error);
        }

        [Fact]
        public async Task It_Updates_The_Student()
        {
            var student = new Student(
                new StudentName("Jordan Walker"),
                new EmailAddress("walker.jlg@gmail.com")
            );
            await studentRepositorySpy.Add(student);

            var command = new EditStudentDetailsCommand(student.Id, "Walker Jordan", "jlg.walker@gmail.com");
            var result = await handler.Handle(command, new CancellationToken());

            Assert.True(result.IsSuccess);
            Assert.Equal("Walker Jordan", student.Name.Name);
            Assert.Equal("jlg.walker@gmail.com", student.Email.Address);
            Assert.True(unitOfWorkSpy.WasCommited);
        }
    }
}
