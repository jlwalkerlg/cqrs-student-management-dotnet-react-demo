using System;
using StudentManagement.Application;
using StudentManagement.Application.Students;
using Xunit;

namespace StudentManagement.ApplicationTests.Students
{
    public class RegisterStudentCommandValidationTests : ValidationTestBase
    {
        private readonly RegisterStudentCommandValidator validator;

        public RegisterStudentCommandValidationTests()
        {
            validator = new RegisterStudentCommandValidator();
        }

        [Fact]
        public void It_Validates_The_Name()
        {
            var command = new RegisterStudentCommand("", "walker.jlg@gmail.com");
            var result = validator.Validate(command);

            AssertHasValidationError(result, "StudentName");
        }

        [Fact]
        public void It_Validates_The_Email()
        {
            RegisterStudentCommand command;
            Result<Guid> result;

            command = new RegisterStudentCommand("Jordan Walker", "");
            result = validator.Validate(command);
            AssertHasValidationError(result, "StudentEmailAddress");

            command = new RegisterStudentCommand("Jordan Walker", "not_a_real_email");
            result = validator.Validate(command);
            AssertHasValidationError(result, "StudentEmailAddress");
        }
    }
}
